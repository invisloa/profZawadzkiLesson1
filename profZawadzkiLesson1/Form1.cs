using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace profZawadzkiLesson1
{
	public partial class Form1 : Form
	{

		StudentsListGenerator listGenerator = new StudentsListGenerator();
		public List<Student> StudentsList = new List<Student>();
		public List<Student> SearchList = new List<Student>();
		public Form1()
		{
			InitializeComponent();
			dgvStudentsList.ColumnCount = 4;
			dgvStudentsList.RowCount = 1;
			dgvStudentsList.Columns[0].HeaderText = "lp.";
			dgvStudentsList.Columns[0].Width = 50;
			dgvStudentsList.Columns[1].HeaderText = "Name";
			dgvStudentsList.Columns[2].HeaderText = "Last Name";
			dgvStudentsList.Columns[3].HeaderText = "Students Id";

			StudentsList = listGenerator.MakeStudentsList();
		}

		private void btnSumbit_Click(object sender, EventArgs e)
		{
			string name = txtBoxName.Text;
			string lastName = txtBoxLastName.Text;
			int studenstId;
			if (string.IsNullOrEmpty(name))
			{
				MessageBox.Show("Name is required.");
				return;
			}

			if (string.IsNullOrEmpty(lastName))
			{
				MessageBox.Show("Last name is required.");
				return;
			}
			if (!int.TryParse(txtBoxStudentsId.Text, out studenstId))
			{
				MessageBox.Show("ID is required.");
				return;
			}


			if (SearchBox.Text == "")
			{
				StudentsList.Add(new Student(name, lastName, studenstId));
			}
			else if (SearchBox.Text.Length > 0 && dgvStudentsList.Rows.Count == 1)
			{
				int searcherdID = Int32.Parse((dgvStudentsList.Rows[0].Cells[3].Value).ToString());
				var studentToChange = StudentsList.FirstOrDefault(x => x.StudentId == searcherdID);
				studentToChange.StudentId = studenstId;
				studentToChange.Name = name;
				studentToChange.LastName = lastName;
			}

			ClearTxtControls();
		}
		private void btnShowStudents_Click(object sender, EventArgs e)
		{
			ShowStudentsFromList(StudentsList);
		}
		void ClearTxtControls()
		{
			txtBoxName.Text = string.Empty;
			txtBoxLastName.Text = string.Empty;
			txtBoxStudentsId.Text = string.Empty;
		}

		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			SearchList.Clear();
			SearchList = StudentsList.Where(x => x.Name.ToLower().Contains(SearchBox.Text.ToLower())
											|| x.LastName.ToLower().Contains(SearchBox.Text.ToLower())
											|| x.StudentId.ToString().Contains(SearchBox.Text.ToLower())).ToList();
			ShowStudentsFromList(SearchList);
		}
		private void SearchBox_LostFocus(object sender, EventArgs e)
		{
			SearchBox.Clear();

		}


		private void btnClearStudentsList_Click(object sender, EventArgs e)
		{
			StudentsList.Clear();
			dgvStudentsList.Rows.Clear();
		}
		void ShowStudentsFromList(List<Student> studentsList)
		{
			dgvStudentsList.Rows.Clear();
			int currentLpNumber = 1;
			if (studentsList != null)
			{
				foreach (Student student in studentsList)
				{
					string[] tab = student.ToString().Split(' ');
					string[] rowItems = new string[] { currentLpNumber.ToString() }.Concat(tab).ToArray();
					dgvStudentsList.Rows.Add(rowItems);
					currentLpNumber++;
				}
			}
		}

	}
}