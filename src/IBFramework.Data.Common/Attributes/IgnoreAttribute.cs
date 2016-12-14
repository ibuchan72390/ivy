using System;

namespace IBFramework.Data.Common.Attributes
{
    /*
     * This attribute is supposed to go on runtime properties maintained in db classes
     * If the property is going to be set during runtime, but does NOT exist in the database,
     * we should decorate it with this attribute.  It will ensure that the SQL generator does not
     * include it when generating the SQL statements.
     * 
     * In any type of NOSQL Database, this field will be useless
     */
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class IgnoreAttribute : Attribute
    {
    }
}
