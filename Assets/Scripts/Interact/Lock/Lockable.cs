using Attributes;
using UnityEngine;

namespace Interact.Lock {
	public abstract class LockableMonoBehaviour : MonoBehaviour {
		// ReSharper disable once InconsistentNaming
		[SerializeField] [ReadOnly] private bool _lock = false;
		
		/// <summary>
		/// "Заблокировать" объект 
		/// </summary>
		/// <returns>Успешность операции, False - объект был закрыт до этого</returns>
		public bool Lock() {
			if (_lock) return false;
			_lock = true;
			return true;
		}

		/// <summary>
		/// "Разблокировать" объект 
		/// </summary>
		/// <returns>Успешность операции, False - объект не был заблокирован до этого</returns>
		public bool Unlock() {
			if (!_lock) return false;
			_lock = false;
			return true;
		}

		/// <summary>
		/// Состояние замка
		/// </summary>
		/// <returns></returns>
		public bool GetLock() {
			return _lock;
		}
	}
}