using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using static Dapper.SqlMapper;

namespace Data.Extensions
{
	public static class DapperExtensions
	{
		private readonly static IEnumerable<Type> _primitives = new List<Type> { typeof(int), typeof(long), typeof(string) };
		
		public static ICustomQueryParameter GetTableValuedParameter<T>(this IEnumerable<T> list, string parameterType = null)
		{
			return (list != null && list.Any())
				? list.ToDataTable().AsTableValuedParameter(parameterType)
				: BlankDataTable(typeof(T)).AsTableValuedParameter(parameterType);
		}

		#region private
		private static DataTable ToDataTable<T>(this IEnumerable<T> list)
		{
			if (list == null || !list.Any())
				return BlankDataTable(typeof(T));
			var properties = TypeDescriptor.GetProperties(typeof(T));
			var table = new DataTable();

			//typeof(T) == typeof(int) || typeof(T) == typeof(long) || typeof(T) == typeof(string))
			if (_primitives.Contains(typeof(T)))
				return ToValueTypeDataTable(list);

			foreach (PropertyDescriptor prop in properties)
				table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

			foreach (var item in list)
			{
				var row = table.NewRow();
				foreach (PropertyDescriptor prop in properties)
					row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
				table.Rows.Add(row);
			}

			return table;
		}

		private static DataTable ToValueTypeDataTable<T>(IEnumerable<T> self)
		{
			const string columnName = "id";
			var table = new DataTable();
			table.Columns.Add("id", typeof(T));
			foreach (var item in self)
			{
				var row = table.NewRow();
				row[columnName] = item;
				table.Rows.Add(row);
			}

			return table;
		}

		private static DataTable BlankDataTable(Type t)
		{
			var dt = new DataTable();
			if (t == typeof(int) || t == typeof(long) || t == typeof(string))
				dt.Columns.Add(new DataColumn("id"));
			else
			{
				var props = TypeDescriptor.GetProperties(t);
				foreach (PropertyDescriptor prop in props)
					dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}
			return dt;
		}
		#endregion
	}
}
