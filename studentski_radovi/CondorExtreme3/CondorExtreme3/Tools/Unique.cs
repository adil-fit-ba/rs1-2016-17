using System;
using System.Linq;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using CondorExtreme3.DAL;
using System.Web;

namespace CondorExtreme3.Tools
{
    public class Unique : ValidationAttribute
    {
        private Type type;
        public Unique(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
        public Unique(string errorMessage, Type value)
        {
            this.ErrorMessage = errorMessage;
            this.type = value;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CondorDBContextChild db = new CondorDBContextChild(HttpContext.Current.Session["ConnectionString"].ToString());

            var className = this.type.Name + "s";
            var propertyName = validationContext.MemberName;
            var parameterName = string.Format("@{0}", propertyName);
            
            
            var result = db.Database.SqlQuery<int>(
                string.Format("SELECT COUNT(*) FROM {0} WHERE {1}={2}", className, propertyName, parameterName),
                new System.Data.SqlClient.SqlParameter(parameterName, value));
            
            if (result.ToList()[0] > 0)
                return new ValidationResult(string.Format(this.ErrorMessage));
            

            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}