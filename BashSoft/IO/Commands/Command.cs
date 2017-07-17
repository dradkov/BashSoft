using BashSoft.Repository;
using BashSoft.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    public abstract class Command
    {
        private string input;
        private string[] data;
        private Tester judje;
        private StudentRepository repository;
        private IOManager inputOutputmaneger;


        public Command(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger)
        {
            this.Input = input;
            this.Data = data;
            this.judje = judje;
            this.repository = repository;
            this.inputOutputmaneger = inputOutputmaneger;
                
        }


        protected string Input
        {
            get
            {
                return this.input;
            }
            private set
            {
                this.input = value;
            }

        }

        protected string[] Data
        {
            get
            {
                return this.data;
            }
            private set
            {
                this.data = value;
            }

        }
        protected Tester Judge
        {
            get
            {
                return this.judje;
            }

        }
        protected StudentRepository Repository
        {
            get
            {
                return this.repository;
            }

        }
        protected IOManager InputOutputmaneger
        {
            get
            {
                return this.inputOutputmaneger;
            }
        }


        public abstract void Execute();
    }
}
