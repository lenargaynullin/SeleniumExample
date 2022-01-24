namespace TestSelenium.patterns
{
    public class Person
    {
        private string name;
        private string surname;
        private string middlename;


        public class Builder
        {
            private Person newPerson;

            public Builder()
            {
                this.newPerson = new Person();
            }

            public Builder WithName(string name)
            {
                newPerson.name = name;
                return this;
            }
            
            public Builder WithSurname(string name)
            {
                newPerson.surname = name;
                return this;
            }
            
            public Builder WithMiddleame(string name)
            {
                newPerson.middlename = name;
                return this;
            }

            public Person build()
            {
                return newPerson;
            }
            
        }
    }
}