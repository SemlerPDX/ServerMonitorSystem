using Microsoft.Scripting.Hosting;
using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Implements the <see cref="IConfigPythonLoad"/> interface to provide access
    /// to a method for loading <see cref="IConfigPython"/> properties from file.
    /// <para>
    /// Dependencies: <see cref="IConfigPython"/>
    /// </para>
    /// </summary>
    class ConfigPythonLoad : IConfigPythonLoad
    {
        /// <summary>
        /// The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.
        /// </summary>
        private readonly IConfigPython _configPython;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigPythonLoad"/> class with the specified interface.
        /// </summary>
        /// <param name="configPython">
        /// The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.
        /// </param>
        public ConfigPythonLoad(IConfigPython configPython)
        {
            _configPython = configPython;
        }

        /// <summary>
        /// Load all ConfigPython class global properties from the config python file.
        /// </summary>
        /// <param name="pythonScope">
        /// The scope used to contain the properties of the <see cref="IConfigPython"/> class
        /// being loaded from file for the Python Engine.</param>
        public void LoadPythonConfigFile(ScriptScope pythonScope)
        {
            // Iterate through each ConfigPython property to set to matching variables in file
            foreach (var property in typeof(IConfigPython).GetProperties())
            {
                var pythonVar = property.Name;
                var pythonValue = pythonScope.GetVariable<object>(pythonVar);

                try
                {
                    var convertedValue = Convert.ChangeType(pythonValue, property.PropertyType);
                    property.SetValue(_configPython, convertedValue);
                }
                catch (InvalidCastException)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        string stringValue = pythonValue?.ToString().Trim('\'');
                        property.SetValue(_configPython, stringValue);
                    }
                    else if (property.PropertyType == typeof(bool))
                    {
                        bool boolValue = (bool)pythonValue;
                        property.SetValue(_configPython, boolValue);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        int intValue = (int)pythonValue;
                        property.SetValue(_configPython, intValue);
                    }
                    else if (property.PropertyType == typeof(long))
                    {
                        long longValue = (long)pythonValue;
                        property.SetValue(_configPython, longValue);
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        double doubleValue = (double)pythonValue;
                        property.SetValue(_configPython, doubleValue);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        float floatValue = (float)pythonValue;
                        property.SetValue(_configPython, floatValue);
                    }
                    else if (property.PropertyType == typeof(decimal))
                    {
                        decimal decimalValue = (decimal)pythonValue;
                        property.SetValue(_configPython, decimalValue);
                    }
                    else if (property.PropertyType == typeof(string[]))
                    {
                        // Remove any custom wrappings and encapsulation parentheses
                        string stringValue = pythonValue?.ToString().Trim('\'').Replace("'", "").Replace("(", "").Replace(")", "");

                        string[] stringArrayValue;
                        if (stringValue.Contains(","))
                        {
                            // is comma deliniated string array
                            stringArrayValue = stringValue.Split(',');
                            for (int i = 0; i < stringArrayValue.Length; i++)
                            {
                                // must ensure no trailing spaces in values
                                stringArrayValue[i] = stringArrayValue[i]?.Trim();
                            }
                            property.SetValue(_configPython, stringArrayValue);
                        }
                        else
                        {
                            // is single element array
                            stringArrayValue = new string[] { stringValue };
                            property.SetValue(_configPython, stringArrayValue);
                        }
                    }
                }
            }
        }

    }
}
