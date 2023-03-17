
using System.Runtime.Serialization;

namespace Bajtpik;

[DataContract(IsReference=true)]
public class Book : IPublication
{
	[DataMember]
	public string Title { get; set; }
	[DataMember]
	public List<Author> Authors { get; set; }
	[DataMember]
	public int Year { get; set; }
	[DataMember]
	public int? PageCount { get; set; }
	
	public void PrintBook()
	{
		Console.WriteLine(Title+ " , " + Year + " , " + PageCount + " , " +
		                  Authors.Select(x => x.Name + " " + x.Surname).Aggregate((x, y) => x + ", " + y));

	}
	
}











	
	
 

