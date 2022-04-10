using Autodesk.Revit.Attributes;
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

    [TransactionAttribute(TransactionMode.Manual)]
    public class CreateRooms : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;


            ICollection<ElementId> roomsId = CreateUtils.CreateRooms(commandData);
            FilteredElementCollector FEC = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms);
            IList<ElementId> roomIds = FEC.ToElementIds() as IList<ElementId>;
            foreach (ElementId roomId in roomIds)
            {
                Element element = doc.GetElement(roomId);
               
                Transaction transaction = new Transaction(doc, "Изменение метки");
                transaction.Start();
                Room room = element as Room;
                string levelName = room.Level.Name.ToString();
                string roomNumber = room.Number.ToString();
                room.Name = $"{levelName}_{roomNumber}";

                transaction.Commit();

            }

            return Result.Succeeded;
        }

    }

}

