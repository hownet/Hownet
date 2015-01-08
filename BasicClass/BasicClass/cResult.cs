using System;
using System.Data;


namespace BasicClass
{
    public delegate void TextChangedHandler(string s);
    public delegate void ChangeRepo(string s,string ty);
    public delegate void RowChangedHandler(DataTable dt);
    public delegate void TableNumChangedHandler(DataSet ds);

    public class cResult
    {
        public string Result1 = "";
        public string Result2 = "";

        public event TextChangedHandler TextChanged;
        public event RowChangedHandler RowChanged;
        public event TableNumChangedHandler TableChanged;
        public event ChangeRepo ChangeRepoed;

        public void ChangeText(string s)
        {
            try
            {
                if (TextChanged != null)
                    TextChanged(s);
            }
            catch (Exception ex)
            {

            }
        }

        public void RowChang(DataTable dt)
        {
            if (RowChanged != null)
                RowChanged(dt);
        }
        public void TableChang(DataSet ds)
        {
            if (TableChanged != null)
                TableChanged(ds); 
        }
        public void RepoChange(string s, string ty)
        {
            if (ChangeRepoed != null)
                ChangeRepoed(s, ty);
        }
    }
}
