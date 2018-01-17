/**************************
**	MenuManager.cs
**	Chase McWhirt
**	January 16th, 2018
***************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiteMenuManager: MonoBehaviour {
	public List<GameObject> menuList;
	
	public void Start() {
		Debug.Log("LiteManager start!!");
		OpenMenu(0, false);
    }
	
	public void OpenMenu(int menuIndex, bool closeMenusFirst) {
		Debug.Log("Opening menu!!");
		
		if(closeMenusFirst == true) {
			CloseAllMenus();
		}
		
		menuList[menuIndex].gameObject.SetActive(true);
    }
	
	public void CloseAllMenus() {
		Debug.Log("Closing all menus!!");
		
		foreach(GameObject menu in menuList) {
			menu.gameObject.SetActive(false);
		}
	}
}
//Use enumerators to abstract the menuIndex from OpenMenu.























        //var topCanvas = menuInstance.GetComponent<Canvas>();
        //var previousCanvas = menuStack.Peek().GetComponent<Canvas>();
		//topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;

		//menuInstance = Instantiate(menuList[menuIndex], transform);
		//Instantiate(prefab, transform);
        //	De-activate top menu
        //if(menuStack.Count > 0) {
		//}


		//if(menuStack.Count == 0) {
		//	Debug.LogErrorFormat("Menu can't be closed because stack is empty.");
		//	return;
		//}
		
		//GameObject closedMenu = menuStack.Pop();
		//Destroy(closedMenu.gameObject);

	/*public void CloseMenu(GameObject menu){
		if (menuStack.Count == 0) {
			Debug.LogErrorFormat(
				menu,
				"{0} cannot be closed because menu stack is empty",
				menu.GetType()
			);
			return;
		}

		if (menuStack.Peek() != menu) {
			Debug.LogErrorFormat(
				menu,
				"{0} cannot be closed because it is not on top of stack",
				menu.GetType()
			);
			return;
		}

		CloseTopMenu();
	}
	
		public void CloseTopMenu() {
        var instance = menuStack.Pop();
		
		if(instance.DestroyWhenClosed) { Destroy(instance.gameObject); }
		else { instance.gameObject.SetActive(false); }

        //	Re-activate top menu
		//	If a re-activated menu is an overlay we need to
		//		activate the menu under it
		foreach(var menu in menuStack) {
            menu.gameObject.SetActive(true);
			if(menu.DisableMenusUnderneath) { break; }
		}
    }*/
//}









/*using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InGameMenuManager: MonoBehaviour {
	public TitleMenu TitleMenuPrefab;
	public SettingsMenu SettingsMenuPrefab;
    public static InGameMenuManager Instance { get; set; }

	private Stack<Menu> menuStack = new Stack<Menu>();
	
	public void CreateInstance<T>() where T: Menu {
		var prefab = GetPrefab<T>();
		Instantiate(prefab, transform);
	}

	public void OpenMenu(Menu instance) {
        //	De-activate top menu
        if(menuStack.Count > 0) {
			if(instance.DisableMenusUnderneath) {
				foreach(var menu in menuStack) {
					menu.gameObject.SetActive(false);
					if(menu.DisableMenusUnderneath) { break; }
				}
			}

            var topCanvas = instance.GetComponent<Canvas>();
            var previousCanvas = menuStack.Peek().GetComponent<Canvas>();
			topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
        }

        menuStack.Push(instance);
    }
	
	public void CloseMenu(Menu menu){
		if (menuStack.Count == 0) {
			Debug.LogErrorFormat(
				menu,
				"{0} cannot be closed because menu stack is empty",
				menu.GetType()
			);
			return;
		}

		if (menuStack.Peek() != menu) {
			Debug.LogErrorFormat(
				menu,
				"{0} cannot be closed because it is not on top of stack",
				menu.GetType()
			);
			return;
		}

		CloseTopMenu();
	}

	public void CloseTopMenu() {
        var instance = menuStack.Pop();
		
		if(instance.DestroyWhenClosed) { Destroy(instance.gameObject); }
		else { instance.gameObject.SetActive(false); }

        //	Re-activate top menu
		//	If a re-activated menu is an overlay we need to
		//		activate the menu under it
		foreach(var menu in menuStack) {
            menu.gameObject.SetActive(true);
			if(menu.DisableMenusUnderneath) { break; }
		}
    }
	
    private T GetPrefab<T>() where T : Menu {
        //	Get prefab dynamically, based on public fields set from Unity
		//	You can use private fields with SerializeField attribute too
        var fields = this.GetType().GetFields(	BindingFlags.Public |
												BindingFlags.Instance |
												BindingFlags.DeclaredOnly);
        foreach(var field in fields) {
            var prefab = field.GetValue(this) as T;
            if(prefab != null) {
                return prefab;
            }
        }

        throw new MissingReferenceException("Prefab not found for type "
											+ typeof(T));
    }

    private void Update() {
        //	On Android the back button is sent as Esc
        if(Input.GetKeyDown(KeyCode.Escape) && menuStack.Count > 0) {
            menuStack.Peek().OnBackPressed();
        }
    }
	
	private void Awake() {
        Instance = this;
		TitleMenu.Show();
    }

    private void OnDestroy() {
        Instance = null;
    }
}*/