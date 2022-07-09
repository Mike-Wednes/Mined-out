using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class EnumExtensions
    {
        public static bool DoesEnumContainKey(this Type enumType, ConsoleKeyInfo key)
        {
            if (enumType.IsEnum)
            {
                var enumMembers = enumType.GetEnumNames();
                string keyString = key.Key.ToString();
                if (enumMembers.Contains(keyString))
                {
                    return true;
                }
            }
            return false;            
        }
    }
}
