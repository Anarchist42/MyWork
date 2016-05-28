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
                Draw.OutputScene();
                SWriter.Close();
            }
            catch { return false; }
            return true;
        }
        #endregion
    }
}