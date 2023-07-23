using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SharedCoreLibrary.Application.Enums;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Application.Models.Request.DynamicSearch;

namespace SharedInfraStructureLibrary.Extensions
{
    public static class Linq
    {
        public static List<string> OrderTypes = new List<string>() { "desc", "asc" };

        // operators
        public static Dictionary<string, string> Opertors = new Dictionary<string, string>
        {
                {"gt",">" },
                {"gte",">=" },
                {"lt","<" },
                {"lte","<=" },
                {"contain","like" },
                {"notContain","notlike" },
                {"equal","==" },
                {"notEqual","!=" },
                {"startsWith","like" },
                {"endsWith","like" },
        };

        public static IQueryable<T> DynamicSearch<T>(this IQueryable<T> source, SearchModel searchModel)
        {
            var isArabicCulture = CultureInfo.CurrentUICulture.Name
                     == CultureCode.ar.ToString();
            List<PropertyInfo> tProperties = typeof(T).GetProperties().ToList();
            if (searchModel.SearchFields != null)
            {

                foreach (SearchFieldModel field in searchModel.SearchFields)
                {
                    try
                    {
                        if (field.IsLocalized)
                        {
                            field.FieldName = isArabicCulture ? field.FieldName + "Ar" : field.FieldName + "En";
                        }
                        var property = GetProperty<T>(field.FieldName);
                        if (!field.FieldName.Contains('.')
                            && !tProperties.Exists(p => p.Name.ToLower() == field.FieldName.ToLower())
                            && property == null)
                        {
                            continue;
                        }

                        if (!string.IsNullOrWhiteSpace(field.Value) && Opertors.Keys.Contains(field.Operator))
                        {
                            Type actualType = property.PropertyType;
                            Type underlyingType = Nullable.GetUnderlyingType(actualType) ?? actualType;

                            switch (field.Operator)
                            {
                                case "contain":
                                    try
                                    {
                                        if (Nullable.GetUnderlyingType(actualType) != null)
                                        {
                                            source = source.Where($"{FirstLetterToUpper($"{field.FieldName}")}.Value.Trim().Contains(@0)", field.Value.Trim());
                                        }
                                        else
                                        {
                                            source = source.Where($"{FirstLetterToUpper($"{field.FieldName}")}.Trim().Contains(@0)", field.Value.Trim());
                                        }
                                    }
                                    catch (Exception) { continue; }
                                    break;
                                case "notContain":
                                    try
                                    {
                                        // nullable fields
                                        if (Nullable.GetUnderlyingType(actualType) != null)
                                        {
                                            source = source.Where($"!{field.FieldName}.Value.ToString().Trim().Contains(@0)", field.Value.Trim());
                                        }
                                        else
                                        {
                                            source = source.Where($"!{field.FieldName}.ToString().Trim().Contains(@0)", field.Value.Trim());
                                        }
                                    }
                                    catch (Exception) { continue; }
                                    break;
                                case "startsWith":
                                    try
                                    {
                                        // nullable fields
                                        if (Nullable.GetUnderlyingType(actualType) != null)
                                        {
                                            source = source.Where($"!{field.FieldName}.Value.ToString().Trim().StartsWith(@0)", field.Value.Trim());
                                        }
                                        else
                                        {
                                            source = source.Where($"!{field.FieldName}.ToString().Trim().StartsWith(@0)", field.Value.Trim());
                                        }
                                    }
                                    catch (Exception) { continue; }
                                    break;
                                case "endsWith":
                                    try
                                    {
                                        // nullable fields
                                        if (Nullable.GetUnderlyingType(actualType) != null)
                                        {
                                            source = source.Where($"!{field.FieldName}.Value.ToString().Trim().EndsWith(@0)", field.Value.Trim());
                                        }
                                        else
                                        {
                                            source = source.Where($"!{field.FieldName}.ToString().Trim().EndsWith(@0)", field.Value.Trim());
                                        }
                                    }
                                    catch (Exception) { continue; }
                                    break;
                                default:
                                    try
                                    {
                                        object safeValue;
                                        // case on timespan
                                        if (actualType == typeof(TimeSpan) || actualType == typeof(TimeSpan?))
                                        {
                                            safeValue = TimeSpan.Parse(field.Value);
                                        }

                                        else
                                        {
                                            safeValue = (field.Value == null) ? null : Convert.ChangeType(field.Value, underlyingType);
                                        }

                                        if ((actualType == typeof(DateTime) || actualType == typeof(DateTime?)))
                                        {

                                            source = DateSearch(safeValue, field, source);
                                        }
                                        else
                                        {
                                            source = source.Where($"{field.FieldName} {Opertors[field.Operator]} @0", safeValue);
                                        }
                                    }
                                    catch (Exception) { continue; }
                                    break;
                            }

                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            searchModel.OrderType = OrderTypes.Contains(searchModel.OrderType) && searchModel.OrderBy != null ? searchModel.OrderType : "desc";
            var localizedField = isArabicCulture ? searchModel.OrderBy + "Ar" : searchModel.OrderBy + "En";
            searchModel.OrderBy = GetProperty<T>(searchModel.OrderBy) != null ? searchModel.OrderBy : GetProperty<T>(localizedField) != null ? localizedField : tProperties.First().Name;
            source = source.OrderBy($"{searchModel.OrderBy} {searchModel.OrderType}");
            return source;
        }

        private static PropertyInfo GetProperty<T>(string propName)
        {
            Type currentType = typeof(T);
            PropertyInfo currentProp = null;
            string[] indexes = !string.IsNullOrEmpty(propName)? propName.Split('.'): new string[0];

            for (int i = 0; i < indexes.Length; i++)
            {
                var prop = indexes[i];
                currentProp = currentType
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.ToLower() == prop.ToLower());
                if (currentProp == null)
                {
                    return null;
                }
                currentType = currentProp.PropertyType;
            }
            return currentProp;
        }
        private static IQueryable<T> DateSearch<T>(object safeValue, SearchFieldModel field, IQueryable<T> source)
        {
            TimeSpan timeSpan;
            var isTimeSpan = TimeSpan.TryParse(safeValue.ToString(), null, out timeSpan);
            var date = DateTime.Parse(safeValue.ToString());
            switch (field.Operator)
            {
                case "equal":
                    try
                    {
                        if (field.SearchType == "TimeSpan")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).TimeOfDay == timeSpan);
                        }
                        else if (field.SearchType == "date")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")) == date);
                        }
                        else
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).Date == date);
                        }
                    }
                    catch (Exception) { }
                    break;
                case "notEqual":
                    try
                    {
                        if (field.SearchType == "TimeSpan")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).TimeOfDay != timeSpan);
                        }
                        else if (field.SearchType == "date")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")) != date);
                        }
                        else
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).Date != date);
                        }
                    }

                    catch (Exception) { }
                    break;
                case "gt":
                    try
                    {
                        if (field.SearchType == "TimeSpan")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).TimeOfDay > timeSpan);
                        }
                        else if (field.SearchType == "date")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")) > date);
                        }
                        else
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).Date > date);
                        }
                    }

                    catch (Exception) { }
                    break;
                case "gte":
                    try
                    {
                        if (field.SearchType == "TimeSpan")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).TimeOfDay >= timeSpan);
                        }
                        else if (field.SearchType == "date")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")) >= date);
                        }
                        else
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).Date >= date);
                        }
                    }

                    catch (Exception) { }
                    break;
                case "lt":
                    try
                    {
                        if (field.SearchType == "TimeSpan")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).TimeOfDay < timeSpan);
                        }
                        else if (field.SearchType == "date")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")) < date);
                        }
                        else
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).Date < date);
                        }
                    }

                    catch (Exception) { }
                    break;
                case "lte":
                    try
                    {
                        if (field.SearchType == "TimeSpan")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).TimeOfDay <= timeSpan);
                        }
                        else if (field.SearchType == "date")
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")) <= date);
                        }
                        else
                        {
                            source = source.Where(a => EF.Property<DateTime>(a, FirstLetterToUpper($"{field.FieldName}")).Date <= date);
                        }
                    }

                    catch (Exception) { }
                    break;
            }

            return source;
        }

        private static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
