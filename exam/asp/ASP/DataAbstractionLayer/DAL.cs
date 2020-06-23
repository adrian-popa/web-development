using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ASP.Models;

namespace ASP.DataAbstractionLayer
{
    public class DAL
    {
        private readonly string MYSQL_CONN_STRING = "Server=192.168.64.2;Port=3306;Uid=root;Database=web_exam;";

        MySqlConnection conn;

        public User Login(string username, string password)
        {
            try
            {
                conn = new MySqlConnection
                {
                    ConnectionString = MYSQL_CONN_STRING
                };
                conn.Open();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM professor WHERE username='" + username + "' AND password='" + password + "'"
                };
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                if (mySqlDataReader.Read())
                {
                    mySqlDataReader.Close();

                    return new User { Username = username, Type = "professor" };
                }
                else
                {
                    mySqlDataReader.Close();

                    cmd.CommandText = "SELECT * FROM student WHERE username='" + username + "' AND password='" + password + "'";
                    mySqlDataReader = cmd.ExecuteReader();

                    if (mySqlDataReader.Read())
                    {
                        mySqlDataReader.Close();

                        return new User { Username = username, Type = "student" };
                    }
                    else
                    {
                        mySqlDataReader.Close();

                        return null;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);

                return null;
            }
        }

        public List<int> GetGroups(string username)
        {
            try
            {
                conn = new MySqlConnection
                {
                    ConnectionString = MYSQL_CONN_STRING
                };
                conn.Open();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM professor WHERE username='" + username + "'"
                };
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                if (mySqlDataReader.Read())
                {
                    int professorId = mySqlDataReader.GetInt32("id");

                    mySqlDataReader.Close();

                    cmd.CommandText = "SELECT * FROM course WHERE professor_id='" + professorId + "'";
                    mySqlDataReader = cmd.ExecuteReader();

                    List<int> groups = new List<int>();

                    while (mySqlDataReader.Read())
                    {
                        groups.Add(mySqlDataReader.GetInt32("group_id"));
                    }

                    mySqlDataReader.Close();

                    return groups;
                }
                else
                {
                    mySqlDataReader.Close();

                    return null;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);

                return null;
            }
        }

        public List<Student> GetStudentsByGroup(int groupId)
        {
            try
            {
                conn = new MySqlConnection
                {
                    ConnectionString = MYSQL_CONN_STRING
                };
                conn.Open();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM student WHERE group_id='" + groupId + "'"
                };
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                List<Student> students = new List<Student>();

                while (mySqlDataReader.Read())
                {
                    Student student = new Student
                    {
                        Id = mySqlDataReader.GetInt32("id"),
                        Username = mySqlDataReader.GetString("username"),
                        GroupId = mySqlDataReader.GetInt32("group_id"),
                        Grade = mySqlDataReader.GetFloat("grade")
                    };

                    students.Add(student);
                }

                mySqlDataReader.Close();

                return students;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);

                return null;
            }
        }

        public float GetGrade(string username)
        {
            try
            {
                conn = new MySqlConnection
                {
                    ConnectionString = MYSQL_CONN_STRING
                };
                conn.Open();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM student WHERE username='" + username + "'"
                };
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                if (mySqlDataReader.Read())
                {
                    float grade = mySqlDataReader.GetFloat("grade");

                    mySqlDataReader.Close();

                    return grade;
                }

                mySqlDataReader.Close();

                return 0;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);

                return 0;
            }
        }

        public bool AssignGrade(int studentId, float newGrade)
        {
            try
            {
                conn = new MySqlConnection
                {
                    ConnectionString = MYSQL_CONN_STRING
                };
                conn.Open();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM student WHERE id='" + studentId + "'"
                };
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                if (mySqlDataReader.Read() && mySqlDataReader.GetInt32("grade") != newGrade)
                {
                    mySqlDataReader.Close();

                    cmd.CommandText = "UPDATE student SET grade='" + newGrade + "' WHERE id='" + studentId + "'";
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);

                return false;
            }
        }
    }
}
