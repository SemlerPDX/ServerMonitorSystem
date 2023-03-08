using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Implements the <see cref="IConfigPythonSetScope"/> interface to provide access
    /// to a method to set the Python Engine Scope for saving to file.
    /// <para>
    /// Dependencies: <see cref="IConfigPython"/>
    /// </para>
    /// </summary>
    class ConfigPythonSetScope : IConfigPythonSetScope
    {
        /// <summary>
        /// The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.
        /// </summary>
        private readonly IConfigPython _configPython;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigPythonSetScope"/> class with the specified interface.
        /// </summary>
        /// <param name="configPython">The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.</param>
        public ConfigPythonSetScope(IConfigPython configPython)
        {
            _configPython = configPython;
        }

        /// <summary>
        /// Sets the properties of the <see cref="IConfigPython"/> class to the Python Engine scope for saving to file.
        /// </summary>
        /// <param name="pythonScope">
        /// The scope used to contain the properties of the <see cref="IConfigPython"/> class being
        /// saved to file for the Python Engine.
        /// </param>
        /// <returns>The PythonScope object containing each property of the <see cref="IConfigPython"/> class.</returns>
        public ScriptScope SetConfigPythonScope(ScriptScope pythonScope)
        {
            try
            {
                var properties = typeof(ConfigPython)
                    .GetProperties()
                    .Where(p => p.CanRead && p.CanWrite)
                    .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<ConfigPropertyAttribute>() })
                    .Where(x => x.Attribute != null)
                    .OrderBy(x => x.Attribute.Position)
                    .Select(x => x.Property);

                foreach (var property in properties)
                {
                    var type = property.PropertyType;
                    var value = property.GetValue(_configPython);

                    if (type == typeof(string))
                    {
                        // Replacing escape character '\' and ensure string is surrounded by only one set of single-quotes
                        value = $"'{((string)value).Replace(@"\", @"\\").Replace("'", "")}'";
                    }
                    else if (type.IsArray)
                    {
                        var elementType = type.GetElementType();
                        var elements = (Array)value;
                        var quotedElements = new List<string>();

                        foreach (var element in elements)
                        {
                            if (elementType == typeof(string))
                            {
                                var strElement = (string)element;
                                if (!strElement.StartsWith("'") && !strElement.EndsWith("'"))
                                {
                                    quotedElements.Add($"'{strElement}'");
                                }
                                else
                                {
                                    quotedElements.Add(strElement);
                                }
                            }
                            else
                            {
                                quotedElements.Add(element.ToString());
                            }
                        }

                        value = $"({string.Join(",", quotedElements)})";
                    }
                    else if (type == typeof(bool))
                    {
                        value = ((bool)value).ToString();
                    }
                    else if (type == typeof(double))
                    {
                        value = ((double)value).ToString();
                    }
                    else if (type == typeof(float))
                    {
                        value = ((float)value).ToString();
                    }
                    else if (type == typeof(int))
                    {
                        value = ((int)value).ToString();
                    }
                    else if (type == typeof(long))
                    {
                        value = ((long)value).ToString();
                    }
                    else if (type == typeof(decimal))
                    {
                        value = ((decimal)value).ToString(CultureInfo.InvariantCulture);
                    }

                    // Cast the property and its value as a string and object
                    string keyName = property.Name;
                    object keyValue = (string)value;

                    // Set each property into the IronPython scope
                    pythonScope.SetVariable(keyName, keyValue);
                }
            }
            catch (Exceptions ex)
            {
                ex.LogError("Failure at SetConfigPythonScope method");
            }
            return pythonScope;
        }

    }
}
