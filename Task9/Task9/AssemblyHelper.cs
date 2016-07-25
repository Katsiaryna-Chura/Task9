using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Task9
{
    public class AssemblyHelper
    {
        Assembly assembly;
        List<Type> types;

        public AssemblyHelper(string path)
        {
            assembly = Assembly.LoadFile(path);
        }

        public List<Type> GetTypes()
        {
            types = assembly.GetTypes().ToList();
            return types;
        }

        public List<MemberInfo> GetFieldsOfType(int index)
        {
            return types[index].GetFields().ToList<MemberInfo>();
        }

        public List<MemberInfo> GetPropertiesOfType(int index)
        {
            return types[index].GetProperties().ToList<MemberInfo>();
        }

        public List<MemberInfo> GetMethodsOfType(int index)
        {
            return types[index].GetMethods().ToList<MemberInfo>();
        }

        public List<MemberInfo> GetAllMembersOfType(int index)
        {
            return types[index].GetMembers().ToList<MemberInfo>();
        }

    }
}
