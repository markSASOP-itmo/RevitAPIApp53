using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPIApp53
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                string tabName = "Мои Плагины";
                application.CreateRibbonTab(tabName);

                string panelName = "Инструменты";
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, panelName);

                string assemblyPath = @"C:\Program Files\RevitAPITraining\";

                PushButtonData btnDashboardData = new PushButtonData(
                    "1",
                    "Статистика проекта",
                    Path.Combine(assemblyPath, "RevitAPIApp51.dll"),
                    "RevitAPIApp51.Main"
                );

                PushButton btnDashboard = ribbonPanel.AddItem(btnDashboardData) as PushButton;
                btnDashboard.ToolTip = "Показать дашборд с количеством труб, объемом стен и дверей.";

                PushButtonData btnChangeTypeData = new PushButtonData(
                    "2",
                    "Изменить тип стен",
                    Path.Combine(assemblyPath, "RevitAPIApp52.dll"),
                    "RevitAPIApp52.Main"
                );

                PushButton btnChangeType = ribbonPanel.AddItem(btnChangeTypeData) as PushButton;
                btnChangeType.ToolTip = "Выбрать стены и изменить их тип через окно WPF.";

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Ошибка", $"Не удалось создать ленту: {ex.Message}");
                return Result.Failed;
            }
        }
    }
}
