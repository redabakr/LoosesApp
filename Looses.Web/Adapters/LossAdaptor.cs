using System.Collections;
using Looses.Web.Models;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace Looses.Web.Adapters;

public class LossAdaptor: DataAdaptor
{
    public List<LossWriteModel> records { get; set; } = new List<LossWriteModel>();
        public override async Task<Object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            IEnumerable GridData = records;
            IEnumerable DataSource = records;
            await Task.Delay(100); //To mimic asynchronous operation, we delayed this operation using Task.Delay
            if (dataManagerRequest.Sorted?.Count > 0) // perform Sorting
            {
                GridData = DataOperations.PerformSorting(GridData, dataManagerRequest.Sorted);
            }
            if (dataManagerRequest.Skip != 0)
            {
                GridData = DataOperations.PerformSkip(GridData, dataManagerRequest.Skip); //Paging
            }
            if (dataManagerRequest.Take != 0)
            {
                GridData = DataOperations.PerformTake(GridData, dataManagerRequest.Take);
            }
            IDictionary<string, object> aggregates = new Dictionary<string, object>();
            if (dataManagerRequest.Aggregates != null) // Aggregation
            {
                aggregates = DataUtil.PerformAggregation(DataSource, dataManagerRequest.Aggregates);
            }
            if (dataManagerRequest.Group != null && dataManagerRequest.Group.Any()) //Grouping
            {
                foreach (var group in dataManagerRequest.Group)
                {
                    GridData = DataUtil.Group<LossWriteModel>(GridData, group, dataManagerRequest.Aggregates, 0, dataManagerRequest.GroupByFormatter);
                }
            }
            return dataManagerRequest.RequiresCounts ? new DataResult() { Result = GridData, Count = records.Count(), Aggregates = aggregates} : (object)GridData;
        }
        public override async Task<Object> InsertAsync(DataManager dataManager, object value, string key)
        {
            await Task.Delay(100); //To mimic asynchronous operation, we delayed this operation using Task.Delay
            records.Insert(0, value as LossWriteModel);
            return value;
        }
        public override async Task<object> RemoveAsync(DataManager dataManager, object value, string keyField, string key)
        {
            await Task.Delay(100); //To mimic asynchronous operation, we delayed this operation using Task.Delay
            int data = (int)value;
            records.Remove(records.Where((item) => item.Key == data).FirstOrDefault());
            return value;
        }
        public override async Task<object> UpdateAsync(DataManager dataManager, object value, string keyField, string key)
        {
            await Task.Delay(100); //To mimic asynchronous operation, we delayed this operation using Task.Delay
            var val = (value as LossWriteModel);
            var data = records.Where((item) => item.Key == val.Key).FirstOrDefault();
            if (data != null) {
                data.WellName = val.WellName;
                data.EventName = val.EventName;
                data.LoosDate = val.LoosDate;
            }
            return value;
        }
}