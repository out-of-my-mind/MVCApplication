using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Infrastructrue
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private readonly int _maxWords;
        //向基类的构造函数传递一个默认的错误提示信息
        public MaxWordsAttribute(int maxWords) : base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }
        //为了实现这个验证逻辑，至少需要重写基类中提供IsValid方法的其中一个版本
        //第一个参数是要验证的对象，如果对象是有效的就返回一个成功的验证结果
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _maxWords)
                {
                    //FormatErrorMessage方法会使用提供的属性名来填充占位符
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}