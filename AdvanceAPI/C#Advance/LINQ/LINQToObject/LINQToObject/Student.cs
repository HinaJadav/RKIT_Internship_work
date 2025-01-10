using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQToObject
{
    /// <summary>
    /// Represents a student with details such as Id, Name, CPI (Cumulative Performance Index), 
    /// Fees, Placement Package, Placement Status, Gender, and Department ID.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the student's Cumulative Performance Index (CPI).
        /// </summary>
        public double CPI { get; set; }

        /// <summary>
        /// Gets or sets the total fees paid by the student.
        /// </summary>
        public int Fees { get; set; }

        /// <summary>
        /// Gets or sets the placement package offered to the student.
        /// </summary>
        public double Package { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the student is placed.
        /// </summary>
        public bool isPlaced { get; set; }

        /// <summary>
        /// Gets or sets the gender of the student.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the department ID associated with the student.
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class with specified details.
        /// </summary>
        /// <param name="id">The unique identifier for the student.</param>
        /// <param name="name">The name of the student.</param>
        /// <param name="cpi">The Cumulative Performance Index of the student.</param>
        /// <param name="fees">The fees paid by the student.</param>
        /// <param name="package">The placement package offered to the student.</param>
        /// <param name="isPlaced">Indicates whether the student is placed.</param>
        /// <param name="gender">The gender of the student.</param>
        /// <param name="departmentID">The department ID associated with the student.</param>
        public Student(int id, string name, double cpi, int fees, double package, bool isPlaced, string gender, int departmentID)
        {
            Id = id;
            Name = name;
            CPI = cpi;
            Fees = fees;
            Package = package;
            this.isPlaced = isPlaced;
            Gender = gender;
            DepartmentID = departmentID;
        }
    }
}
