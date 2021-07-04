using System;
using System.Collections.Generic;
using System.Text;
using MyGeneration.dOOdads;

namespace LC.Model.BMS.BLL
{
   public class tblDayComments : LC.Model.BMS.DAL._tblDayComments
    {
       public void getDayComment(int year, int month, int day)
       {
           string query = string.Empty;
           query += " select dc.* from dbo.tblDayComments dc ";
           query += " where  year(dc.CommentDate) = '" + year + "' and month(dc.CommentDate) = '" + month + "' and day(dc.CommentDate) = '" + day + "' "; ;
           this.LoadFromRawSql(query);
       }
    }
}
