using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel : MonoBehaviour {

    private bool init_ = false;

	public virtual void Init () {
		
	}

	public virtual void Show () {
		if (!init_) {
            Init();
            init_ = true;
        }
		
        this.gameObject.SetActive(true);
    }

	public virtual void Hide () {
		this.gameObject.SetActive(false);
	}
}
