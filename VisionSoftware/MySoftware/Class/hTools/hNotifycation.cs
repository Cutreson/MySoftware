using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MySoftware.Class.hTools
{
	public class SvNotifyCollection<T> : List<T>
	{
		public delegate void AddItemEventHandler(object sender, T item);
		private event SvNotifyCollection<T>.AddItemEventHandler _AddItem;
		public event SvNotifyCollection<T>.AddItemEventHandler AddItem
		{
			[CompilerGenerated]
			add
			{
				SvNotifyCollection<T>.AddItemEventHandler addItemEventHandler = this._AddItem;
				SvNotifyCollection<T>.AddItemEventHandler addItemEventHandler2;
				do
				{
					addItemEventHandler2 = addItemEventHandler;
					SvNotifyCollection<T>.AddItemEventHandler value2 = (SvNotifyCollection<T>.AddItemEventHandler)Delegate.Combine(addItemEventHandler2, value);
					addItemEventHandler = Interlocked.CompareExchange<SvNotifyCollection<T>.AddItemEventHandler>(ref this._AddItem, value2, addItemEventHandler2);
				}
				while (addItemEventHandler != addItemEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SvNotifyCollection<T>.AddItemEventHandler addItemEventHandler = this._AddItem;
				SvNotifyCollection<T>.AddItemEventHandler addItemEventHandler2;
				do
				{
					addItemEventHandler2 = addItemEventHandler;
					SvNotifyCollection<T>.AddItemEventHandler value2 = (SvNotifyCollection<T>.AddItemEventHandler)Delegate.Remove(addItemEventHandler2, value);
					addItemEventHandler = Interlocked.CompareExchange<SvNotifyCollection<T>.AddItemEventHandler>(ref this._AddItem, value2, addItemEventHandler2);
				}
				while (addItemEventHandler != addItemEventHandler2);
			}
		}
		private event EventHandler _ClearCollection;
		public event EventHandler ClearCollection
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this._ClearCollection;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this._ClearCollection, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this._ClearCollection;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this._ClearCollection, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}
		public new void Add(T item)
		{
			base.Add(item);
			this.NotifyAddItem(item);
		}
		public new void Clear()
		{
			base.Clear();
			bool flag = this._ClearCollection != null;
			if (flag)
			{
				this._ClearCollection(this, null);
			}
		}
		protected void NotifyAddItem(T item)
		{
			bool flag = this._AddItem != null;
			if (flag)
			{
				this._AddItem(this, item);
			}
		}
	}
}
