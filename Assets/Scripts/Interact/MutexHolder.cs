using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interact {
	public class MutexHolderLockedEventArgs {
		public Mutex mutex;
		public MutexHolderLockedEventArgs(Mutex mutex) {
			this.mutex = mutex;
		}
	}
	
	public class MutexHolderLockedEvent : UnityEvent<MutexHolderLockedEventArgs> { }

	public class MutexHolderUnlockedEventArgs {
		public Mutex mutex;
		public MutexHolderUnlockedEventArgs(Mutex mutex) {
			this.mutex = mutex;
		}
	}
	public class MutexHolderUnlockedEvent : UnityEvent<MutexHolderUnlockedEventArgs> { }
	
	[Serializable]
	public class MutexHolder {

		[SerializeReference] [ReadOnly] private List<Mutex> mutexList = new List<Mutex>(); 
		public MutexHolderLockedEvent holderLocked = new MutexHolderLockedEvent();
		public MutexHolderUnlockedEvent holderUnlocked = new MutexHolderUnlockedEvent();

		/// <summary>
		/// Холдеру передается объект из мутекса, исключитльно для события (пока что)
		/// </summary>
		public void Lock(Mutex byMutex) {
			mutexList.Add(byMutex);
			holderLocked.Invoke(new MutexHolderLockedEventArgs(byMutex));
		}
		
		/// <summary>
		/// Освободить мутекс текущим холдером
		/// </summary>
		/// <returns>Если мутекс не был открыт - False</returns>
		public bool Unlock(Mutex byMutex) {
			if (!mutexList.Contains(byMutex)) {
				Debug.LogError($"Trying to unlock mutex {byMutex} that didn't locked by current holder {this}");
				return false;
			}
			
			if (!byMutex.TryUnlock(this)) return false;
			holderUnlocked.Invoke(new MutexHolderUnlockedEventArgs(byMutex));
			return true;
		}
	}
}