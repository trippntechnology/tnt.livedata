using System;

namespace TNT.LiveData
{
	/// <summary>
	/// Transforms the <see cref="LiveData{T}"/>
	/// </summary>
	public static class Transformations
	{
		/// <summary>
		/// Transforms the value in <paramref name="source"/> using <paramref name="func"/>
		/// and returns the new value as <see cref="LiveData{R}"/>
		/// </summary>
		/// <typeparam name="T">Type of object managed by <paramref name="source"/></typeparam>
		/// <typeparam name="R">Type of object returned by <paramref name="func"/></typeparam>
		/// <param name="source"><see cref="LiveData{T}"/> being transformed</param>
		/// <param name="func"><see cref="Func{T, TResult}"/> that transforms the value in <paramref name="source"/>
		/// to a value of <typeparamref name="R"/></param>
		/// <returns><see cref="LiveData{R}"/> that represents the transformed data</returns>
		public static LiveData<R> Map<T, R>(LiveData<T> source, Func<T, R> func)
		{
			var newLiveData = new LiveData<R>();

			source.OnChanged += (value) =>
			{
				var result = func(value);
				newLiveData.Value = result;
			};

			newLiveData.Value = func(source.Value);
			return newLiveData;
		}

		/// <summary>
		/// Transforms the value in <paramref name="source"/> into <see cref="LiveData{R}"/> using 
		/// <paramref name="func"/> and returns the new <see cref="LiveData{R}"/>
		/// </summary>
		/// <typeparam name="T">Type of object managed by <paramref name="source"/></typeparam>
		/// <typeparam name="R">Type of object returned by <paramref name="func"/></typeparam>
		/// <param name="source"><see cref="LiveData{T}"/> being transformed</param>
		/// <param name="func"><see cref="Func{T, TResult}"/> that transforms the value in <paramref name="source"/>
		/// to a value of <typeparamref name="R"/></param>
		/// <returns><see cref="LiveData{R}"/> that represents the transformed data</returns>
		public static LiveData<R> SwitchMap<T, R>(LiveData<T> source, Func<T, LiveData<R>> func)
		{
			var liveData = func(source.Value);

			source.OnChanged += (value) =>
			{
				liveData = func(value);
			};

			return liveData;
		}
	}
}
