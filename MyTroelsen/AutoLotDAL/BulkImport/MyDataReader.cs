﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AutoLotDAL.BulkImport
{
    public class MyDataReader<T> : IMyDataReader<T>
    {
        int _currentIndex;
        private readonly PropertyInfo[] _propertyInfos;
        private readonly Dictionary<string,int> _nameDictionary;

        public object this[int i] => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public List<T> Records { get; set; }

        public int Depth => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();

        public int FieldCount => _propertyInfos.Length;


        public MyDataReader(IEnumerable<T> records)
        {
            _propertyInfos = typeof(T).GetProperties();
            _nameDictionary = _propertyInfos
                .Select((x, index) => new { x.Name, index })
                .ToDictionary(pair => pair.Name, pair => pair.index);
            Records = (List < T >)records;

        }


        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            return i > 0 && i < FieldCount ? _propertyInfos[i].Name : string.Empty ;
        }

        public int GetOrdinal(string name)
        {
            return _nameDictionary.ContainsKey(name) ? _nameDictionary[name] : -1;
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            return _propertyInfos[i].GetValue(Records[_currentIndex]);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            if ((_currentIndex + 1) >= Records.Count) return false;
            _currentIndex++;

            return true;
        }
    }

}
