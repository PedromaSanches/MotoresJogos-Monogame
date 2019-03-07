namespace Sanches.Pooling
{
	public interface IPool<T>
	{
		T GetInstance();
	}
}