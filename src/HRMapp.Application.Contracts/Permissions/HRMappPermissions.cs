namespace HRMapp.Permissions;

public static class HRMappPermissions
{
    public const string GroupName = "HRMapp";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public class Employee
    {
        public const string Default = GroupName + ".Employee";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class Department
    {
        public const string Default = GroupName + ".Department";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class Shift
    {
        public const string Default = GroupName + ".Shift";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class Contact
    {
        public const string Default = GroupName + ".Contact";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class Contract
    {
        public const string Default = GroupName + ".Contract";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class Salary
    {
        public const string Default = GroupName + ".Salary";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class Attendent
    {
        public const string Default = GroupName + ".Attendent";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class AttendentLine
    {
        public const string Default = GroupName + ".AttendentLine";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class AttendentForMonth
    {
        public const string Default = GroupName + ".AttendentForMonth";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class EmployeeHistory
    {
        public const string Default = GroupName + ".EmployeeHistory";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
