using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace androbot
{
    class DatabaseSaver
    {
        public void CreateHelper(String path, ObservableCollection<AndroTable> tables)
        {
            path += "\\src\\DBHelper.java";
            if (File.Exists(path))
            {
                MessageBox.Show("File already exists...");
            }
            else
            {
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(HelperOpening());
                sw.WriteLine(HelperPrivateVars(tables));
                sw.WriteLine("\n\tprivate Context context;\n");
                sw.WriteLine("\n\tprivate static final String DATABASE_NAME = \"database.db\";\n");
                sw.WriteLine("\n\tprivate static final int DATABASE_VERSION = 1;\n");
                sw.WriteLine("\n\tprivate static DBHelper instance;\n");
                sw.WriteLine(HelperSingleton());
                sw.WriteLine(HelperOnCreate(tables));
                sw.WriteLine(databaseExists());
                sw.WriteLine(onUpgrade(tables));
                sw.WriteLine("\n}");
                sw.Close();
            }
        }
        private String HelperOpening()
        {
            return "import java.io.File;\nimport android.content.Context;\nimport android.database.sqlite.SQLiteDatabase;\nimport android.database.sqlite.SQLiteOpenHelper;\n\npublic class DBHelper extends SQLiteOpenHelper {\n";
        }
        private String HelperPrivateVars(ObservableCollection<AndroTable> tables)
        {
            String output = "";
            foreach (AndroTable table in tables)
            {
                output += "\tpublic static final String TABLE_" + table.Name.ToUpper() + " = \"" + table.Name + "\";\n";
                foreach (AndroProperty prop in table.Properties)
                {
                    output += "\tpublic static final String COLUMN_" + table.Name.ToUpper() + "_" + prop.Name.ToUpper() + " = \"" + prop.Name + "\";\n";
                }
            }
            return output;
        }
        private String HelperSingleton()
        {
            String output = "";
            output += "\n\tpublic static DBHelper getInstance(Context context) {";
            output += "\n\t\tif(instance==null) {";
            output += "\n\t\t\tinstance = new DBHelper(context);";
            output += "\n\t\t}";
            output += "\n\t\treturn instance;";
            output += "\n\t}";
            output += "\n";
            output += "\n\tprivate DBHelper(Context context) {";
            output += "\n\t\tsuper(context, DATABASE_NAME, null, DATABASE_VERSION);";
            output += "\n\t\tthis.context = context;";
            output += "\n\t}";
            return output;
        }
        private String HelperOnCreate(ObservableCollection<AndroTable> tables)
        {
            String output = "\n\n\t@Override";
            output += "\n\tpublic void onCreate(SQLiteDatabase db) {";
            foreach (AndroTable table in tables)
            {
                output += "\n\t\tString create" + table.Name + "SQL = \"create table \" + TABLE_" + table.Name.ToUpper() +
                          " + \"(\" + ";
                int i = 0;
                foreach (AndroProperty prop in table.Properties)
                {
                    output += "COLUMN_" + table.Name.ToUpper() + "_" + prop.Name.ToUpper() + " + \" " + TypeConverter(prop.Type);
                    if (i == 0) output += " primary key autoincrement";
                    if (i < table.Properties.Count - 1)
                    {
                        output += ", ";
                    }
                    output += "\" + ";
                    i++;
                }
                output += "\")\";";
                output += "\n\t\tdb.execSQL(create" + table.Name + "SQL);";
            }
            output += "\n\t}";
            return output;
        }
        private String databaseExists()
        {
            return "\n\tpublic Boolean databaseExists() {\n\t\tFile dbFile = context.getDatabasePath(DATABASE_NAME);\n\t\treturn dbFile.exists();\n\t}";
        }
        private String onUpgrade(ObservableCollection<AndroTable> tables)
        {
            String output = "\n\t@Override";
            output += "\n\tpublic void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {";
            foreach (AndroTable table in tables)
            {
                output += "\n\t\tdb.execSQL(\"DROP TABLE IF EXISTS \" + TABLE_" + table.Name.ToUpper() + ");";
            }
            output += "\n\t\tonCreate(db);";
            output += "\n\t}";
            return output;
        }

        public static String TypeConverter(String objectProp)
        {
            String dbColumn = objectProp.ToLower();
            switch (objectProp.ToLower())
            {
                case "int":
                    dbColumn = "integer";
                    break;
                case "string":
                    dbColumn = "text";
                    break;
            }
            return dbColumn;
        }
    }
}
