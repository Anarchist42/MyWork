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
        public WorkingWithFiles() { } 
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
                string[] Str = SReader.ReadToEnd().Split('\r', '\n');
                List<string> Strin = new List<string>();
                /*Удаляем лишние*/
                foreach (string St in Str)
                    if (St != "") Strin.Add(St);
                /*Разбиваем строку на имя и коэффициент*/
                int i, j;
                string Name;
                for (i = 0; i < Strin.Count; i++)
                {
                    Str = Strin[i].Split(' ');
                    Name = "";
                    for (j = 0; j < Str.Length - 1; j++)
                        Name += Str[j] + " ";
                    Name = Name.TrimEnd(' ');
                    StringsIn.Add(Name);
                    StringsIn.Add(Str[j]);
                }
                SReader.Close();
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Вывод данных сцены в файл.
        /// </summary>
        /// <param name="FileName"> Имя файла. </param>
        /// <param name="Draw"> Объект класса отрисовки. </param>
        /// <param name="MaterialLayer"> Массив материалов слоя. </param>
        /// <param name="MaterialMineral"> Массив материалов минералов. </param>
        /// <returns> Выполнил или нет. </returns>
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
                    SWriter.WriteLine("M " + Color[i] + " " + Material[i] + " P " + Points[i]);
                SWriter.Close();
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Ввод данных сцены из файла.
        /// </summary>
        /// <param name="FileName"> Имя файла. </param>
        /// <param name="Draw"> Объект класса отрисовки. </param>
        /// <param name="MaterialLayer"> Массив материалов слоя. </param>
        /// <param name="MaterialMineral"> Массив материалов минералов. </param>
        /// <returns> Выполнил или нет. </returns>
        public bool InputScene(string FileName, Paint Draw, out List<Material> MaterialLayer, out List<Material> MaterialMineral)
        {
            MaterialLayer = new List<Material>();
            MaterialMineral = new List<Material>();
            try
            {
                FileStream FStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader SReader = new StreamReader(FStream);
                /*Считываем данные и бьем их на части*/
                string[] Str = SReader.ReadToEnd().Split('\r', '\n');
                List<string> StringIn = new List<string>();
                string Color;
                string Material;
                string Points;
                /*Удаляем лишние*/
                foreach (string St in Str)
                    if (St != "") StringIn.Add(St);
                int i, j;
                for (i = 0; i < StringIn.Count; i++)
                {
                    Str = StringIn[i].Split(' ');
                    if (Str[0] == "A") GlobalConst.Accuracy = Convert.ToInt32(Str[1]);
                    if (Str[0] == "W") Draw.XAREASIZE = Convert.ToDouble(Str[1]);
                    if (Str[0] == "H") Draw.YAREASIZE = Convert.ToDouble(Str[1]);
                    if (Str[0] == "E") Draw.EARTHSIZE = Convert.ToDouble(Str[1]);
                    if (Str[0] == "Ml")
                    {
                        Material = "";
                        for (j = 1; j < Str.Length - 1; j++)
                            Material += Str[j] + " ";
                        Material = Material.TrimEnd(' ');
                        MaterialLayer.Add(new Material(Material, Str[j]));
                    }
                    if (Str[0] == "Mm")
                    {
                        Material = "";
                        for (j = 1; j < Str.Length - 1; j++)
                            Material += Str[j] + " ";
                        Material = Material.TrimEnd(' ');
                        MaterialMineral.Add(new Material(Material, Str[j]));
                    }
                    if (Str[0] == "L")
                    {
                        Color = Str[1] + " " + Str[2] + " " + Str[3];
                        j = 4;
                        Material = "";
                        while (Str[j] != "P")
                            Material += Str[j++] + " ";
                        Points = "";
                        j++;
                        for (; j < Str.Length; j++)
                            Points += Str[j] + " ";
                        Draw.InputScene(1, Color, Material, Points);
                    }
                    if (Str[0] == "M")
                    {
                        Color = Str[1] + " " + Str[2] + " " + Str[3];
                        j = 4;
                        Material = "";
                        while (Str[j] != "P")
                            Material += Str[j++] + " ";
                        Points = "";
                        j++;
                        for (; j < Str.Length; j++)
                            Points += Str[j] + " ";
                        Draw.InputScene(2, Color, Material, Points);
                    }
                }
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// Вывод данных под электроразведку.
        /// </summary>
        /// <param name="FileName"> Имя файла. </param>
        /// <param name="Draw"> Объекта класса отрисовки. </param>
        /// <param name="MaterialLayer"> Массив материалов слоя. </param>
        /// <param name="MaterialMineral"> Массив материалов минераловю </param>
        /// <returns></returns>
        public bool OutputMKE(string FileName, Paint Draw, List<Material> MaterialLayer, List<Material> MaterialMineral)
        {
            try
            {
                FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                int i;
                /*Выводим материал слоя*/
                sw.WriteLine("Material");
                for (i = 0; i < MaterialLayer.Count; i++)
                    sw.WriteLine((i + 1).ToString() + " " + MaterialLayer[i].NAME + " " + MaterialLayer[i].RESISTANCE.ToString());
                sw.WriteLine();
                /*Выводим материалы минерала*/
                for (i = 0; i < MaterialMineral.Count; i++)
                    sw.WriteLine((i + 1 + MaterialLayer.Count).ToString() + " " + MaterialMineral[i].NAME + " " + MaterialMineral[i].RESISTANCE.ToString());
                sw.WriteLine();
                /*Выводим массив точек Х*/
                sw.WriteLine("Massive");
                List<double> PointX;
                Draw.OutputKEX(out PointX);
                sw.Write("X ");
                for (i = 0; i < PointX.Count; i++)
                    sw.Write((PointX[i]).ToString() + " ");
                sw.WriteLine();
                /*Выводим массив точек Y*/
                List<double> PointY;
                Draw.OutputKEY(out PointY);
                sw.Write("Y ");
                for (i=0;i<PointY.Count;i++)
                    sw.Write((PointY[i]).ToString() + " ");
                sw.WriteLine();
                sw.WriteLine();
                /*Вывод конечных элементов*/
                List<string> KE;
                Draw.OutputKE(MaterialLayer, MaterialMineral, PointX, PointY, out KE);
                sw.WriteLine("MassiveKE");
                for (i = 0; i < KE.Count; i++)
                    sw.WriteLine("KE " + KE[i]);
                sw.Close();
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}