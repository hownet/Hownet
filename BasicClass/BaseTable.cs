using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BasicClass
{
    public class BaseTable
    {
        private static DataTable _dtPro;
        public static DataTable dtPro
        {
            set
            {
                dtPro = value;
            }
            get
            {
                if (_dtPro == null)
                    _dtPro = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllChinaZone, "GetList", new object[] { "(OrderNum=2)" }).Tables[0];
                    return _dtPro;
            }
        }

        private static DataTable _dtCity;
        public static DataTable dtCity
        {
            set
            {
                dtCity = value;
            }
            get
            {
             if(_dtCity==null)
                _dtCity= BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllChinaZone, "GetList", new object[] { "(OrderNum=3)" }).Tables[0];
             return _dtCity;
            }
        }
        private static DataTable _dtCou;
        public static DataTable dtCou 
        {
            set{
                dtCou=value;
            }
            get{
                if(_dtCou==null)
                    _dtCou= BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllChinaZone, "GetList", new object[] { "(OrderNum=4)" }).Tables[0];
                return _dtCou;
            }
        }
    }
}
