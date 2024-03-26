using System.Runtime.Serialization;

namespace TourismPlaces.ViewModels.Dashboard
{
	[DataContract]
	public class DataPointChart
	{
		public DataPointChart(string label, double y)
		{
			this.Label = label;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}
