using System;
using System.Collections.Generic;
using System.IO;

namespace Second
{
    class WorkingWithFiles
    {
        #region Поля класса
        #endregion

        #region Конструктор
        public WorkingWithFiles()
        {            
        }
        #endregion

        #region GET'S and SET's
        #endregion

        #region Методы
        /// <summary>
        /// Добавление новых материалов.
        /// </summary>
        /// <param name="FileName"> Путь к файлу. </param>
        /// <param name="StringsIn"> Массив данных. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool AddMaterial(string FileName,out List<string> StringsIn)
        {
            StringsIn = new List<string>();
            try
            {
                FileStream FStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader SReader = new StreamReader(FStream);
                /*Считываем данные и бьем их на части*/
                string[] Str = SReader.ReadToEnd().Split(' ', '\r', '\n');
                /*Удаляем лишние*/
                foreach (string St in Str)
                    if (St != "") StringsIn.Add(St);
                SReader.Close();
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Вывод данных сцены в файл.
        /// </summary>
        /// <param name="FileName"> Имя файла. </param>
        /// <param name="Draw"> </param>
        /// <param name="MaterialLayer"></param>
        /// <param name="MaterialMineral"></param>
        /// <returns></returns>
        public bool OutputScene(string FileName, Paint Draw, List<Material> MaterialLayer, List<Material> MaterialMineral)
        {
            try
            {
                int i;
                FileStream FStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter SWriter = new StreamWriter(FStream);
                /*Вывод точности, ширины области, высоты области, высоты над землей*/
                SWriter.WriteLine("A " + GlobalConst.Accuracy);
                SWriter.WriteLine("W " + Draw.XAREASIZE.ToString());
                SWriter.WriteLine("H " + Draw.YAREASIZE.ToString());
                SWriter.WriteLine("E " + Draw.EARTHSIZE.ToString());
                /*Выводим материал слоя*/
                for (i = 0; i < MaterialLayer.Count; i++)
                    SWriter.WriteLine("Ml " + MaterialLayer[i].NAME + " " + MaterialLayer[i].RESISTANCE.ToString());
                /*Выводим материалы минерала*/
                for (i = 0; i < MaterialMineral.Count; i++)
                    SWriter.WriteLine("Mm " + MaterialMineral[i].NAME + " " + MaterialMineral[i].RESISTANCE.ToString());
                List<string> Color, Material, Points;
                /*Получаем данные по слоям*/
                Draw.OutputScene(1, out Color, out Material, out Points);
                /*Выводим слои*/
                for (i = 0; i < Color.Count; i++)
                    SWriter.WriteLine("L " + Color[i] + " " + Material[i] + " " + Points[i]);
                /*Получаем данные по минералам*/
                Draw.OutputScene(2, out Color, out Material, out Points);
                /*Выводим данные по минералам*/
                for (i = 0; i < Color.Count; i++)
                    SWriter.WriteLine("M " + Color[i] + " " + Material[i] + " " + Points[i]);
                SWriter.Close();
            }
            catch { return false; }
            return true;
        }

        public bool InputScene(string FileName, Paint Draw, out List<Material> MaterialLayer, out List<Material> MaterialMineral)
        {
            MaterialLayer = new List<Material>();
            MaterialMineral = new List<Material>();
            try
            {
                FileStream FStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader SReader = new StreamReader(FStream);
                /*Считываем данные и бьем их на части*/
                string[] Str = SReader.ReadToEnd().Split(' ', '\r', '\n');
                List<string> StringIn = new List<string>();
                string Color;
                string Material;
                string Points;
                /*Удаляем лишние*/
                foreach (string St in Str)
                    if (St != "") StringIn.Add(St);
                int i;
                for(i=0;i<StringIn.Count;i++)
                {
                    if (StringIn[i] == "A") GlobalConst.Accuracy = Convert.ToInt32(StringIn[++i]);
                    if (StringIn[i] == "W") Draw.XAREASIZE = Convert.ToDouble(StringIn[++i]);
                    if (StringIn[i] == "H") Draw.YAREASIZE = Convert.ToDouble(StringIn[++i]);
                    if (StringIn[i] == "E") Draw.EARTHSIZE = Convert.ToDouble(StringIn[++i]);
                    if (StringIn[i] == "Ml") MaterialLayer.Add(new Material(StringIn[++i], StringIn[++i]));
                    if (StringIn[i] == "Mm") MaterialMineral.Add(new Material(StringIn[++i], StringIn[++i]));
                    if (StringIn[i] == "L")
                    {
                        Color = StringIn[++i] + " " + StringIn[++i] + " " + StringIn[++i];
                        Material = StringIn[++i] + " " + StringIn[++i];
                        Points = "";
                        i++;
                        while(i<StringIn.Count && StringIn[i]!="M" && StringIn[i]!="L")
                            Points += StringIn[i++] + " ";
                        i--;
                        Draw.InputScene(1, Color, Material, Points);
                    }
                    if (StringIn[i] == "M")
                    {
                        Color = StringIn[++i] + " " + StringIn[++i] + " " + StringIn[++i];
                        Material = StringIn[++i] + " " + StringIn[++i];
                        Points = "";
                        i++;
                        while (i < StringIn.Count && StringIn[i] != "M" && StringIn[i] != "L")
                            Points += StringIn[i++] + " ";
                        i--;
                        Draw.InputScene(2, Color, Material, Points);
                    }
                }
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}