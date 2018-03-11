/* Interface para os estados que são gerenciados pelo FSM.cs
 */

using UnityEngine;
using System.Collections;

public interface IFSMState {
	//Metodo para quando entra em um estado
	IEnumerator Enter();
	//Metodo para quando sai de um estado
	IEnumerator Exit();
	//Metodo chamado a cada frame
	void FSMUpdate();
}
