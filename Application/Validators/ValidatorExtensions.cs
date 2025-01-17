﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T,string> Password<T>(this IRuleBuilder<T,string> ruleBuilder)
        {
            var options = ruleBuilder.NotEmpty()
                .MinimumLength(6).WithMessage("Your Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Your Password must contain at least 1 uppercase character")
                .Matches("[a-z]").WithMessage("Your Password must contain at least 1 lowercase character")
                .Matches("[0-9]").WithMessage("Your Password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Your Password must contain a non-alphanumeric character");
            return options;
        }
    }
}
