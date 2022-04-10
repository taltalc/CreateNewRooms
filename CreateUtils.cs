using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNewRooms
{
    class CreateUtils
    {
        public static ICollection<ElementId> CreateRooms(ExternalCommandData commandData)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Level> levels = GetUtils.GetLevels(commandData);
            
            if (levels==null)
            {
                TaskDialog.Show("Ошибка","Не найдены уровни");
            }
            ICollection<ElementId> rooms=null;
            
            Transaction ts = new Transaction(doc, "Добавление помещений");
            
            ts.Start();
            foreach (Level level in levels)
            {

              rooms = doc.Create.NewRooms2(level, GetUtils.GetPhase(commandData));
            
            }
                

            ts.Commit();

            return rooms;
        }
        
    }
}
