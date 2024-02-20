using UnityEngine;

public abstract class Entity
{
    private Vector2 _position;
    public virtual Vector2 position {
        get => _position;
        set => _position = value;   
    }

    private float _rotation;
    public float rotation {
        get => _rotation;
        protected set => _rotation = value;
    }

    protected bool isDead;

    public delegate void OnDeath(Entity sender);
    public event OnDeath OnDeathEvent;

    public delegate void OnDestruction(Entity sender);
    public event OnDestruction OnDestructionEvent;
    protected readonly Level level;
    public Entity(Level level) {
        this.level = level;
        level.AddEntity(this);
    }

    public virtual void Update(float delta) {

    }

    public void Kill(bool silent = false) {
        if (isDead) return;
        isDead = true;

        if (!silent) {
            Death();
            OnDestructionEvent?.Invoke(this); //Событие для репрезентаций
        }

        OnDeathEvent?.Invoke(this);
        OnDeathEvent = null;
        ObligatoryOnRemove();
    }

    protected virtual void ObligatoryOnRemove() {
        //На случай, если внутри модели есть связь с этой энтити, которую необходимо разорвать перед удалением
    }

    protected virtual void Death() {
        //Действия внутри модели, возникающие в результате смерти (пр. взрыв бочки -> дамаг по окружающим)
    }
}
