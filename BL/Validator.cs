using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Task3.BL
{
    /// <summary>
    /// Represents input fields validator.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// A list of inputs to validate.
        /// </summary>
        private readonly List<TextBox> _inputs;

        private readonly TextBox _emailInput;

        private readonly TextBox _phoneNumberInput;

        /// <summary>
        /// Constructs a new validator object with given input fields.
        /// </summary>
        /// <param name="inputs">A list of inputs.</param>
        /// <param name="emailInput">Email input.</param>
        /// <param name="phoneNumberInput">Phone number input.</param>
        public Validator(List<TextBox> inputs, TextBox emailInput, TextBox phoneNumberInput)
        {
            _inputs = inputs;
            _emailInput = emailInput;
            _phoneNumberInput = phoneNumberInput;
        }

        /// <summary>
        /// Validates input fields given in constructor.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws if some field(s) contains errors.</exception>
        public void Validate()
        {
            foreach (var input in _inputs)
            {
                var parent = input.Parent as GroupBox;
                var result = Validation.GetErrors(input);
                if (result.Count > 0)
                {
                    if (parent != null)
                    {
                        throw new InvalidDataException("Field '" + parent.Header + "' contains errors!");
                    }

                    throw new InvalidDataException("Some fields contains errors!");
                }

                if (input.Text.Trim().Length != 0)
                {
                    continue;
                }

                if (parent != null)
                {
                    throw new InvalidDataException("Field '" + parent.Header + "' can't be empty!");
                }

                throw new InvalidDataException("Check all fields if it is filled up!");
            }

            ValidateEmail();
            ValidatePhoneNumber();
        }

        /// <summary>
        /// Validates an email field.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws if email field contains errors.</exception>
        private void ValidateEmail()
        {
            var text = _emailInput.Text.Trim();
            if (text.Length < 1)
            {
                throw new InvalidDataException("Field 'Email' can't be empty!");
            }

            if (!new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").IsMatch(text))
            {
                throw new InvalidDataException("Incorrect email pattern!");
            }
        }

        /// <summary>
        /// Validates an phone number field.
        /// </summary>
        /// <exception cref="InvalidDataException">Throws if phone number field contains errors.</exception>
        private void ValidatePhoneNumber()
        {
            var text = _phoneNumberInput.Text.Trim();
            if (text.Length < 1)
            {
                throw new InvalidDataException("Field 'PhoneNumber' can't be empty!");
            }

            if (!new Regex(@"^(\d){3}\-(\d){2}\-(\d){2}\-(\d){3}$").Match(text).Success)
            {
                throw new InvalidDataException("Incorrect phone number pattern, use 'xxx-xx-xx-xxx' format.");
            }
        }
    }
}