
namespace Post.Cmd.Infrastruture.Config
{
    public class MongoDbConfig
    {
        public required string ConnectionString { get; set; }
        public required string Database {  get; set; }

        public required string Collection {  get; set; }
    }
}
