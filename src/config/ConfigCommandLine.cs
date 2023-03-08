using System;
using System.Collections.Generic;
using System.Reflection;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Implements the <see cref="IConfigCommandLine"/> interface to provide access
    /// to a method for processing command line arguments.
    /// <para>
    /// Dependencies: <see cref="IConfigPython"/>
    /// </para>
    /// </summary>
    public class ConfigCommandLine : IConfigCommandLine
    {
        /// <summary>
        /// The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.
        /// </summary>
        private readonly IConfigPython _configPython;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigCommandLine"/> class with the specified interface.
        /// </summary>
        /// <param name="configPython">The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.</param>
        public ConfigCommandLine(IConfigPython configPython)
        {
            _configPython = configPython;
        }

        /// <summary>
        /// Parse command line arguments and cast values to matching <see cref="IConfigPython"/> class properties.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        /// <returns>A HashSet of strings containing the names of any properties changed.</returns>
        public HashSet<string> Parse(string[] args)
        {
            if (args == null || args.Length <= 0)
                return null;

            try
            {
                HashSet<string> processedNames = new();
                foreach (string arg in args)
                {
                    string[] parts = new string[0];
                    try
                    {
                        parts = arg.Split(new char[] { '=' }, 2);
                        if (parts.Length != 2)
                        {
                            throw new Exceptions("Invalid command line argument format used and ignored");
                        }
                    }
                    catch (Exceptions ex)
                    {
                        ex.LogError("Error in commandline argument format at '='");
                        continue;
                    }

                    // Get command line argument name, check for validity
                    string name = parts[0].Trim().ToLower().Replace("-", "");
                    if (String.IsNullOrWhiteSpace(name))
                    {
                        Exceptions.LogMessage("Invalid command line argument name used and ignored");
                        continue;
                    }

                    // Get command line argument value, check for validity
                    string value = parts[1].Trim();
                    if (String.IsNullOrWhiteSpace(value))
                    {
                        Exceptions.LogMessage("Invalid command line argument value used and ignored: " + name);
                        continue;
                    }

                    // Use reflection to find a matching property by name
                    PropertyInfo property = typeof(IConfigPython).GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property == null)
                    {
                        Exceptions.LogMessage("Unknown command line argument used and ignored: " + name);
                        continue;
                    }

                    // Check if the name has already been processed
                    if (processedNames.Contains(name))
                        Exceptions.LogMessage("Repeated command line argument used for: " + name);

                    // Convert the value to the correct type and set the property value
                    object typedValue;
                    try
                    {
                        if (property.PropertyType.IsArray)
                        {
                            string[] arrayParts = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            typedValue = Array.CreateInstance(property.PropertyType.GetElementType(), arrayParts.Length);
                            try
                            {
                                for (int i = 0; i < arrayParts.Length; i++)
                                {
                                    object elementValue = Convert.ChangeType(arrayParts[i], property.PropertyType.GetElementType());
                                    ((Array)typedValue).SetValue(elementValue, i);
                                }
                            }
                            catch (Exceptions ex)
                            {
                                ex.LogError("Error - Invalid array type ignored - command line arg expected array in Parse method");
                                continue;
                            }
                        }
                        else
                        {
                            typedValue = Convert.ChangeType(value, property.PropertyType);
                        }

                        // Set the property value and add the name of this arg to the set of processed args
                        processedNames.Add(name);
                        property.SetValue(_configPython, typedValue);

                    }
                    catch (Exceptions ex)
                    {
                        ex.LogError("Error setting final property value of command line arg in Parse method");
                        continue;
                    }

                }

                if (processedNames.Count != 0)
                    return processedNames;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at Parse command line arguments method");
            }
            return null;
        }

    }
}
