using System.Collections;

public abstract class State {

    //set variables
    public virtual IEnumerator Initialize() {
        yield break;
    }

    public virtual IEnumerator UserInput() {
        yield break;
    }

    //update
    public virtual IEnumerator Update() {
        yield break;
    }

    //Enter State
    public virtual IEnumerator Enter() {
        yield break;
    }

    public virtual IEnumerator Exit() {
        yield break;
    }

    public virtual IEnumerator Start() {
        yield break;
    }

}