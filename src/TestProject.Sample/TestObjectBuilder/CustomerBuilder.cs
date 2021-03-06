﻿namespace TestProject.Sample.TestObjectBuilder
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Builds a customer object
    /// </summary>
    public class CustomerBuilder
    {
        private string firstname;
        private string lastname;
        private DateTime birthdate;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBuilder"/> class.
        /// </summary>
        public CustomerBuilder()
        {
            // default values
            this.firstname = "John";
            this.lastname = "Doe";
            this.birthdate = new DateTime(2000, 01, 01);
        }

        /// <summary>
        /// Sets the first name.
        /// </summary>
        /// <param name="firstname">The first name.</param>
        /// <returns>A customer instance with the specified first name</returns>
        public CustomerBuilder WithFirstname(string firstname)
        {
            this.firstname = firstname;
            return this;
        }

        /// <summary>
        /// Sets the last name.
        /// </summary>
        /// <param name="lastname">The last name.</param>
        /// <returns>A customer instance with the specified last name</returns>
        public CustomerBuilder WithLastname(string lastname)
        {
            this.lastname = lastname;
            return this;
        }

        /// <summary>
        /// Sets the birthdate.
        /// </summary>
        /// <param name="birthdate">The birthdate.</param>
        /// <returns>A customer instance with the specified birthdate</returns>
        public CustomerBuilder WithBirthdate(DateTime birthdate)
        {
            this.birthdate = birthdate;
            return this;
        }

        /// <summary>
        /// Creates a customer instance.
        /// </summary>
        /// <returns>A customer instance with the specified properties</returns>
        public Customer Create()
        {
            return new Customer(this.firstname, this.lastname, this.birthdate);
        }

        /// <summary>
        /// A sample customer object. Replace this by an actual domain object to test.
        /// </summary>
        public class Customer : INotifyPropertyChanged
        {
            private string firstname;
            private string lastname;
            private DateTime birthdate;

            /// <summary>
            /// Initializes a new instance of the <see cref="Customer"/> class.
            /// </summary>
            /// <param name="firstname">The firstname.</param>
            /// <param name="lastname">The lastname.</param>
            /// <param name="birthdate">The birthdate.</param>
            public Customer(string firstname, string lastname, DateTime birthdate)
            {
                this.firstname = firstname;
                this.lastname = lastname;
                this.birthdate = birthdate;
            }

            /// <summary>
            /// Occurs when a property is changed.
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Gets or sets the first name.
            /// </summary>
            public string FirstName
            {
                get
                {
                    return this.firstname;
                }

                set
                {
                    if (this.firstname != value)
                    {
                        this.firstname = value;
                        OnPropertyChanged(nameof(FirstName));
                    }
                }
            }

            /// <summary>
            /// Gets or sets the last name.
            /// </summary>
            public string LastName
            {
                get
                {
                    return this.lastname;
                }

                set
                {
                    if (this.lastname != value)
                    {
                        this.lastname = value;
                        OnPropertyChanged(nameof(LastName));
                    }
                }
            }

            /// <summary>
            /// Determines whether the customer is over-18 on a specified date.
            /// </summary>
            /// <param name="currentDate">The current date.</param>
            /// <returns>
            ///   <c>true</c> if the customer is over-18 on the specified current date; otherwise, <c>false</c>.
            /// </returns>
            public bool IsAdult(DateTime currentDate)
            {
                return this.birthdate.AddYears(18).CompareTo(currentDate) <= 0;
            }

            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}