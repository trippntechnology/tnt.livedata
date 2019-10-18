using System;

namespace TNT.LiveData
{
	/// <summary>
	/// Manages data <see cref="Value"/> that should be treated live
	/// </summary>
	/// <typeparam name="T">Type of the live data</typeparam>
	public class LiveData<T>
	{
		/// <summary>
		/// Backing field
		/// </summary>
		private T _Value = default(T);

		/// <summary>
		/// Delegate set by <see cref="Transformations"/> and <see cref="Observe(Action{T})"/> that is 
		/// called when <see cref="Value"/> changes
		/// </summary>
		internal Action<T> OnChanged { get; set; } = (t) => { };

		/// <summary>
		/// The value managed by this <see cref="LiveData{T}"/>. When set, the <see cref="OnChanged"/>
		/// delegate is called.
		/// </summary>
		public T Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				OnChanged(value);
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public LiveData() { }

		/// <summary>
		/// Initializes <see cref="Value"/>
		/// </summary>
		/// <param name="value">Initial value</param>
		public LiveData(T value) { this.Value = value; }

		/// <summary>
		/// Sets an observable delegate on this <see cref="LiveData{T}"/> that is called when
		/// <see cref="Value"/> changes
		/// </summary>
		/// <param name="observer"><see cref="Action"/> that is called when <see cref="Value"/>
		/// changes</param>
		public void Observe(Action<T> observer)
		{
			OnChanged += observer;
			observer(this.Value);
		}

		public void AddSource<S>(LiveData<S> source, Action<S> action)
		{
			//sources.Add(source);
			source.OnChanged += action;
		}
	}
}
