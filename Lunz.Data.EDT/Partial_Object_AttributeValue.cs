using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Data.EDT
{
    partial class Object_AttributeValue
    {
        public object ReturnValue
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public object GetValue()
        {
            object result = null;
            switch (this.Object_AttributeFiled.DataType)
            {
                case DataTypes.String:
                    result = this.StringValue;
                    break;
                case DataTypes.Integer:
                    result = this.IntValue;
                    break;
                case DataTypes.Float:
                    result = this.FloatValue;
                    break;
                case DataTypes.Boolean:
                    result = this.BoolValue;
                    break;
                case DataTypes.DateTime:
                    result = this.DateTimeValue;
                    break;
                case DataTypes.Object:
                    result = this.Value;
                    break;
                default:
                    result = this.StringValue;
                    break;
            }
            return result;
        }

        public void SetValue(object value)
        {
            if (this.Object_AttributeFiled != null)
            {
                switch (this.Object_AttributeFiled.DataType)
                {
                    case DataTypes.Float:
                        this.FloatValue = value == null ? default(double?) : (double?)Convert.ToDouble(value);
                        break;
                    case DataTypes.Integer:
                        this.IntValue = value == null ? default(int?) : (int?)Convert.ToInt32(value);
                        break;
                    case DataTypes.DateTime:
                        this.DateTimeValue = value == null ? default(DateTime?) : (DateTime?)Convert.ToDateTime(value);
                        break;
                    case DataTypes.Boolean:
                        this.BoolValue = value == null ? default(bool?) : (bool?)Convert.ToBoolean(value);
                        break;
                    case DataTypes.String:
                    default:
                        this.StringValue = value == null ? null : value.ToString();
                        break;
                }
            }
        }
    }
}
