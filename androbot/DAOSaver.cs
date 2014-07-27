using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace androbot
{
    class DAOSaver
    {
        public static void CreateDAO(String path, AndroTable table)
        {
            path += "\\src\\" + table.Name + "DAO.java";
            if (File.Exists(path))
            {
                MessageBox.Show("File already exists...");
            }
            else
            {
                StreamWriter sw = new StreamWriter(path);
                sw.Write("\nimport java.util.ArrayList;");
                sw.Write("\nimport java.util.List;");
                sw.Write("\nimport android.content.ContentValues;");
                sw.Write("\nimport android.content.Context;");
                sw.Write("\nimport android.database.Cursor;");
                sw.Write("\nimport android.database.SQLException;");
                sw.Write("\nimport android.database.sqlite.SQLiteDatabase;");
                sw.Write("\n");
                sw.Write("\npublic class " + table.Name + "DAO {");
                sw.Write("\n\tprivate SQLiteDatabase db;");
                sw.Write("\n\tprivate DBHelper dbHelper;");

                sw.Write("\n\tprivate String[] " + table.Name.ToLower() + "Columns = {");
                int i = 0;
                foreach (AndroProperty prop in table.Properties)
                {
                    sw.Write("DBHelper.COLUMN_" + table.Name.ToUpper() + "_" + prop.Name.ToUpper());
                    if(i<table.Properties.Count-1) sw.Write(", ");
                    i++;
                }
                sw.Write("};");

                sw.Write("\n\tpublic " + table.Name + "DAO(Context context) {");
                sw.Write("\n\t\tdbHelper = DBHelper.getInstance(context);");
                sw.Write("\n\t}");

                sw.Write("\n\tpublic void open() throws SQLException {");
                sw.Write("\n\t\tdb = dbHelper.getWritableDatabase();");
                sw.Write("\n\t}");

                sw.Write("\n\tpublic " + table.Name + " create" + table.Name + "(" + table.Name + " x) {");
                sw.Write("\n\t\tContentValues values = new ContentValues();");
                i = 0;
                foreach (AndroProperty prop in table.Properties)
                {
                    if(i>0)
                    sw.WriteLine("\n\t\tvalues.put(DBHelper.COLUMN_" + table.Name.ToUpper() + "_" + prop.Name.ToUpper() + ", x.get" + prop.Name + "());");
                    i++;
                }
                sw.Write("\n\t\tlong insertId = db.insert(DBHelper.TABLE_" + table.Name.ToUpper() + ", null, values);");
                sw.Write("\n\t\tCursor cursor = db.query(DBHelper.TABLE_" + table.Name.ToUpper() + ", " + table.Name.ToLower() + "Columns, DBHelper.COLUMN_" + table.Name.ToUpper() + "_" + table.Properties[0].Name.ToUpper() + " + \" = \" + insertId, null, null, null, null);");
                sw.Write("\n\t\tcursor.moveToFirst();");
                sw.Write("\n\t\t" + table.Name + " new" + table.Name + " = cursorTo" + table.Name + "(cursor);");
                sw.Write("\n\t\tcursor.close();");
                sw.Write("\n\t\treturn new" + table.Name + ";");
                sw.Write("\n\t}");

                sw.Write("\n\tpublic List<" + table.Name + "> getAll" + table.Name + "() {");
                sw.Write("\n\t\tList<" + table.Name + "> " + table.Name.ToLower() + "s = new ArrayList<" + table.Name + ">();");
                sw.Write("\n\t\tCursor cursor = db.query(DBHelper.TABLE_" + table.Name.ToUpper() + ", " + table.Name.ToLower() + "Columns, null, null, null, null, null);");
                sw.Write("\n\t\tcursor.moveToFirst();");
                sw.Write("\n\t\twhile(!cursor.isAfterLast()) {");
                sw.Write("\n\t\t\t" + table.Name + " " + table.Name.ToLower() + " = cursorTo" + table.Name + "(cursor);");
                sw.Write("\n\t\t\t" + table.Name.ToLower() + "s.add(" + table.Name.ToLower() + ");");
                sw.Write("\n\t\t\tcursor.moveToNext();");
                sw.Write("\n\t\t}");
                sw.Write("\n\t\tcursor.close();");
                sw.Write("\n\t\treturn " + table.Name.ToLower() + "s;");
                sw.Write("\n\t}");

                sw.Write("\n\tpublic void update" + table.Name + "(" + table.Name + " x) {");
                sw.Write("\n\t\tContentValues values = new ContentValues();");
                i = 0;
                foreach (AndroProperty prop in table.Properties)
                {
                    if(i>0)
                    sw.Write("\n\t\tvalues.put(DBHelper.COLUMN_" + table.Name.ToUpper() + "_" + prop.Name.ToUpper() + ", x.get" + prop.Name + "());");
                    i++;
                }
                if(table.Properties.Count>0)
                sw.Write("\n\t\tdb.update(DBHelper.TABLE_" + table.Name.ToUpper() + ", values, DBHelper.COLUMN_" + table.Name.ToUpper() + "_" + table.Properties[0].Name.ToUpper() + " + \"=\" + x.get" + table.Properties[0].Name + "(), null);");
                sw.Write("\n\t}");

                sw.Write("\n\tpublic void delete" + table.Name + "(" + table.Name + " x) {");
                if(table.Properties.Count>0)
                    sw.Write("\n\t\tdb.delete(DBHelper.TABLE_" + table.Name.ToUpper() + ", DBHelper.COLUMN_" + table.Name.ToUpper() + "_" + table.Properties[0].Name.ToUpper() + " + \"=\" + x.get" + table.Properties[0].Name + "(), null);");
                sw.Write("\n\t}");

                sw.Write("\n\tprivate " + table.Name + " cursorTo" + table.Name + "(Cursor cursor) {");
                sw.Write("\n\t\t" + table.Name + " x = new " + table.Name + "();");
                i = 0;
                foreach (AndroProperty prop in table.Properties)
                {
                    sw.WriteLine("\n\t\tx.set" + prop.Name + "(cursor.get" + UppercaseFirst(prop.Type) + "(" + i + "));");
                    i++;
                }
                sw.Write("\n\t\treturn x;");
                sw.Write("\n\t}");

                sw.Write("\n\tpublic void close() {");
                sw.Write("\n\t\tdbHelper.close();");
                sw.Write("\n\t}");

                sw.Write("\n}");

                sw.Close();
            }
        }
        public static String UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
