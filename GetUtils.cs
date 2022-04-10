using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNewRooms
{
    class GetUtils

    {
        public static List<Level> GetLevels(ExternalCommandData commandData)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Level> levels = new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_Levels)
            .OfType<Level>()
            .Where(t=>t.Name.Contains("Уровень"))
            .ToList();

            return levels;
        }
        public static Phase GetPhase(ExternalCommandData commandData)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            Phase phase = new FilteredElementCollector(doc)
            .OfClass(typeof(Phase))
            .OfType<Phase>()
            .Where(t => t.Name.Equals("Стадия 1"))
                .FirstOrDefault();

            return phase;

        }

        
    }
}
