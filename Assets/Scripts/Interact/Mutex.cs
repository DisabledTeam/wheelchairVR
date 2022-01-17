using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interact {
	public class ReadOnlyAttribute : PropertyAttribute { }

	public class MutexLockedEventArgs {
		public MutexHolder holder;

		public MutexLockedEventArgs(MutexHolder holder) {
			this.holder = holder;
		}
	}

	public class MutexLockedEvent : UnityEvent<MutexLockedEventArgs> { }

	public class MutexUnlockedEventArgs {
		public MutexHolder holder;

		public MutexUnlockedEventArgs(MutexHolder holder) {
			this.holder = holder;
		}
	}

	public class MutexUnlockedEvent : UnityEvent<MutexUnlockedEventArgs> { }

	[Serializable]
	public class Mutex {
		[SerializeField] [ReadOnly] private bool _busy;

		public bool busy {
			get => _busy;
			private set => _busy = value;
		}

		[SerializeReference] [ReadOnly] private MutexHolder _holder;

		public MutexHolder holder {
			get => _holder;
			private set => _holder = value;
		}

		public MutexLockedEvent mutexLocked = new MutexLockedEvent();
		public MutexUnlockedEvent mutexUnlocked = new MutexUnlockedEvent();

		/// <summary>
		/// Попытка занять мутекс холдером.
		/// </summary>
		/// <returns>True/False, False выдает Debug.LogError</returns>
		public bool TryLock(MutexHolder byHolder) {
			if (busy) {
				Debug.LogError($"Trying to lock busy Mutex {this} by {byHolder}");
				return false;
			}

			busy = true;
			holder = byHolder;
			mutexLocked.Invoke(new MutexLockedEventArgs(holder));
			holder.Lock(this);
			return true;
		}

		/// <summary>
		/// Попытка освободить мутекс холдером. Может освободить только тот, кто занял.
		/// </summary>
		/// <returns>True/False, False выдает Debug.LogError</returns>
		public bool TryUnlock(MutexHolder byHolder) {
			if (!busy) {
				Debug.LogError($"Trying to unlock free Mutex {this} by {byHolder}");
				return false;
			}

			if (!byHolder.Equals(holder)) {
				Debug.LogError($"Trying to unlock Mutex {this} with wrong holder {byHolder}");
				return false;
			}

			busy = false;
			holder = null;
			mutexUnlocked.Invoke(new MutexUnlockedEventArgs(byHolder));
			return true;
		}
	}
}