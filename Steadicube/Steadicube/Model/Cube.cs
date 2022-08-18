namespace Steadicube.Model
{
    public class Cube
    {
        public double Height { get; set; } = 0;
        public double Width { get; set; } = 0;
        public double Longitude { get; set; } = 0;

        Position position { get; set; } = new Position();
    }
}
