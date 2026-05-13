using CsvHelper.TypeConversion;
using DentistryAppointmentPrac.Shared.Models;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports
{
    public static class PartialImportHelper
    {
        public static void UpdateIfMappedByClass<T>(
            ImportProcess process,
            object recordBatchItem,
            T? existingItem) where T : class
        {
            var recordProperties = recordBatchItem.GetType().GetProperties();
            var existingProperties = existingItem?.GetType().GetProperties() ?? [];

            for(int i = 0; i < recordProperties?.Length; i++)
            {
                var recordProperty = recordProperties[i];
                var existingProperty = existingProperties.FirstOrDefault(x => x.Name == recordProperty.Name);

                if (existingProperty == null || existingItem == null)
                    continue;

                object? newValue = recordProperty.GetValue(recordBatchItem);
                object? currentValue = existingProperty.GetValue(existingItem);

                bool isPropertyString = existingProperty.PropertyType == typeof(string);
                bool isPropertyBoolean = existingProperty.PropertyType == typeof(bool) || existingProperty.PropertyType == typeof(bool?);
                bool isPropertyDate = existingProperty.PropertyType == typeof(DateTime) || existingProperty.PropertyType == typeof(DateTime?);
                bool isPropertInt = existingProperty.PropertyType == typeof(int) || existingProperty.PropertyType == typeof(int?);

                if (isPropertyString)
                    UpdateIfMappedString(process, recordProperty.Name, (string?)currentValue, v => existingProperty.SetValue(existingItem, v), (string?)newValue);
                else if (isPropertyBoolean)
                {
                    if (newValue is bool boolValue)
                        UpdateIfMappedBool(process, recordProperty.Name, (bool?)currentValue, v => existingProperty.SetValue(existingItem, v), boolValue);
                    
                }
                else if (isPropertInt)
                {
                    if (newValue is int intValue)
                        UpdatedIfMappedInt(process, recordProperty.Name, (int?)currentValue, v => existingProperty.SetValue(existingItem, v), intValue);
                }
                else if (isPropertyDate)
                {
                    if (newValue is DateTime dateValue)
                        UpdatedIfMappedDate(process, recordProperty.Name, (DateTime?)currentValue, v => existingProperty.SetValue(existingItem, v), dateValue);
                }
            }
        }
        private static void UpdateIfMappedString(
            ImportProcess process, 
            string propertyName, 
            string? currentValue,
            Action<string?> setter,
            string? newValue)
        {
            if (!process.Mapping.ContainsKey(propertyName))
                return;

            if (!string.Equals(currentValue, newValue))
                setter(newValue ?? "");
        }

        private static void UpdateIfMappedBool(
            ImportProcess process,
            string propertyName, 
            bool? currentValue,
            Action<bool?> setter,
            bool? newValue)
        {
            if (!process.Mapping.ContainsKey(propertyName))
                return;

            if (currentValue != newValue)
                setter(newValue);
        }

        private static void UpdatedIfMappedInt(
            ImportProcess process,
            string propertyName,
            int? currentValue,
            Action<int?> setter,
            int? newValue)
        {
            if (!process.Mapping.ContainsKey(propertyName))
                return;

            if (currentValue != newValue)
                setter(newValue);
        }

        private static void UpdatedIfMappedDate(
            ImportProcess process,
            string propertyName,
            DateTime? currentValue,
            Action<DateTime?> setter,
            DateTime newValue)
        {
            if (!process.Mapping.ContainsKey(propertyName))
                return;

            if (currentValue != newValue)
                setter(newValue);
        }
    }
}
