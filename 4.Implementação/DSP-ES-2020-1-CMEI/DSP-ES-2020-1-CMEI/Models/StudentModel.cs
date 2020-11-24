using Aplicacao;
using Dominio;
using DSP_ES_2020_1_CMEI.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace DSP_ES_2020_1_CMEI.Models
{
    public class StudentModel : Student
    {
        /* View attr
        */

        public List<StudentModel> listStudentModel { get; set; }


        /* Business rules

 */

        private StudentAplication appStudent;

        public List<StudentModel> ListStudent(int idLoginAccess)
        {
            appStudent = new StudentAplication();
            List<StudentModel> listStudent = new List<StudentModel>();

            try
            {
                foreach (DataRow linha in appStudent.QuerieStudent(idLoginAccess).Rows)
                {
                    StudentModel obj = new StudentModel();

                    obj.idStudent = Convert.ToInt32(linha["idStudent"]);
                    obj.nameStudent = Convert.ToString(linha["nameStudent"]);
                    obj.registrationNumber = Convert.ToString(linha["registrationNumber"]);
                    obj.birthDate = Convert.ToDateTime(linha["birthDate"]);

                    listStudent.Add(obj);
                }

                return listStudent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ImportStudent(StudentModel studentModel, HttpFileCollectionBase files)
        {
            try
            {
                List<Student> list = new List<Student>();
                string msgReturn = "";

                if (files != null)
                {
                    // Tratamento de arquivo, verifica se tem documento e converte para ArrayBytes
                    string[] fieldNames = files.AllKeys;

                    for (int i = 0; i < fieldNames.Length; ++i)
                    {
                        HttpPostedFileBase file = files[i];
                        string field = fieldNames[i];

                        if (!file.FileName.Equals(""))
                        {
                            string fileName = files[i].FileName; //O caminho para o arquivo no computador empresa
                            Stream stream = files[i].InputStream; //Os dados reais do arquivo

                            //Converte o arquivo que está num formato stream para byte[]
                            byte[] bytes = new byte[stream.Length];
                            stream.Read(bytes, 0, bytes.Length);

                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                            //gen the byte array into the memorystream
                            using (MemoryStream ms = new MemoryStream(bytes))
                            using (ExcelPackage package = new ExcelPackage(ms))
                            {
                                //get the first sheet from the excel file
                                ExcelWorksheet sheet = package.Workbook.Worksheets[0];

                                int column = 0;
                                int row = 0;
                                Student student = new Student();

                                foreach (var item in sheet.Cells)
                                {
                                    if (column == 3)
                                    {
                                        row++;

                                        if (row > 1)
                                        {
                                            list.Add(student);
                                        }

                                        student = new Student();
                                        column = 0;
                                    }

                                    column++;

                                    if (row >= 1)
                                    {
                                        if (column == 1)
                                        {
                                            student.registrationNumber = Convert.ToString(item.Value);
                                        }
                                        else if (column == 2)
                                        {
                                            student.nameStudent = Convert.ToString(item.Value);
                                        }
                                        else if (column == 3)
                                        {
                                            student.birthDate = Convert.ToDateTime(item.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (list.Count > 0)
                {
                    appStudent = new StudentAplication();
                    msgReturn = appStudent.InsertListStudent(list, studentModel.idLoginAccess);
                }
                else
                {
                    msgReturn = Message.ErrorSpreadsheetStudent;
                }

                return msgReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}