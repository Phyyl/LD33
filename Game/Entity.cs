using Game.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class Entity
    {
        private Map map;
        private float angle;

        public Vector2 Position;
        public Map Map
        {
            get { return map; }
            set
            {
                map?.RemoveEntity(this);
                map = value;
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = MathUtil.Mod(value, 360);
            }
        }

        private bool moving;
        public virtual bool Moving { get { return moving; } }

        public abstract Vector2 Size { get; }
        public abstract bool Solid { get; }

        public virtual RectangleF CollisionBox => new RectangleF(Position.X - Size.X / 2, Position.Y - Size.Y / 2, Size.X, Size.Y);

        public Vector2 DirectionVector => new Vector2((float)Math.Cos(MathHelper.DegreesToRadians(Angle)), (float)Math.Sin(MathHelper.DegreesToRadians(Angle))).Normalized();

        public abstract void Update(float delta);
        public abstract void Render(float delta);

        public Entity(Vector2 position)
        {
            Position = position;
        }

        public virtual void OnCollision(Entity entity, Direction direction) { }

        protected void RenderTexture(Texture texture, Color? color = null)
        {
            texture.Render(Position, texture.Size / 2, Angle, color);
        }

        public void Move(Vector2 movement)
        {
            Vector2 previousPosition = Position;

            Vector2 newMovement = movement;
            Entity collisionEntity = null;
            Direction direction = default(Direction);

            if (movement.Length != 0)
            {
                foreach (var entity in Map.Entities)
                {
                    if (entity.Solid)
                    {
                        Vector2 newNewMovement = MoveWithCollisions(newMovement, entity.CollisionBox, ref direction);
                        if (newMovement != newNewMovement)
                        {
                            collisionEntity = entity;
                        }
                        newMovement = newNewMovement;
                    }
                }

                Position += newMovement;

                collisionEntity?.OnCollision(this, direction);


                float rest = movement.Length - newMovement.Length;

                if (rest > 0)
                {
                    float xBest = -1;
                    float yBest = -1;
                    Entity best = null;

                    foreach (var entity in Map.Entities)
                    {
                        if (entity.Solid)
                        {
                            if (movement.X != 0)
                            {
                                Vector2 xMovement = MoveWithCollisions(new Vector2(rest * Math.Sign(movement.X), 0), entity.CollisionBox, ref direction);

                                if (xMovement.Length < xBest || xBest == -1)
                                {
                                    xBest = xMovement.Length;
                                }
                            }

                            if (movement.Y != 0)
                            {
                                Vector2 yMovement = MoveWithCollisions(new Vector2(0, rest * Math.Sign(movement.Y)), entity.CollisionBox, ref direction);
                                if (yMovement.Length < yBest || yBest == -1)
                                {
                                    yBest = yMovement.Length;
                                }
                            }
                        }
                    }

                    if (xBest > yBest)
                    {
                        if (movement.X > 0)
                        {
                            Position.X += xBest;
                        }
                        else if (movement.X < 0)
                        {
                            Position.X -= xBest;
                        }
                    }
                    else if (yBest != -1)
                    {
                        if (movement.Y > 0)
                        {
                            Position.Y += yBest;
                        }
                        else if (movement.Y < 0)
                        {
                            Position.Y -= yBest;
                        }
                    }

                    best?.OnCollision(this, direction);
                }

                Angle = (float)(Math.Atan2(movement.Y, movement.X) / Math.PI * 180);
            }

            moving = previousPosition != Position;
        }

        private Vector2 MoveWithCollisions(Vector2 movement, RectangleF collisionBox, ref Direction direction)
        {
            collisionBox.Inflate(Size.X / 2, Size.Y / 2);

            Vector2 end = Position + movement;
            Vector2 newMovement = movement;

            if (movement.X > 0) // Right
            {
                if (Position.X >= collisionBox.Right)
                {
                    return movement;
                }
                else if (Position.X > collisionBox.Left)
                {
                    //TODO: Move the player out the box? Let im free?
                    newMovement.X = 0;
                }
                else if (end.X > collisionBox.Left)
                {
                    newMovement.X = collisionBox.Left - Position.X;
                    direction = Direction.Left;
                }
                else
                {
                    return movement;
                }
            }
            else if (movement.X < 0) // Left
            {
                if (Position.X <= collisionBox.Left)
                {
                    return movement;
                }
                else if (Position.X < collisionBox.Right)
                {
                    //TODO: Move the player out the box? Let im free?
                    newMovement.X = 0;
                }
                else if (end.X < collisionBox.Right)
                {
                    newMovement.X = collisionBox.Right - Position.X;
                    direction = Direction.Right;
                }
                else
                {
                    return movement;
                }
            }
            else if (Position.X <= collisionBox.Left || Position.X >= collisionBox.Right)
            {
                return movement;
            }

            if (movement.Y > 0) // Down
            {
                if (Position.Y >= collisionBox.Bottom)
                {
                    return movement;
                }
                else if (Position.Y > collisionBox.Top)
                {
                    //TODO: Move the player out the box? Let im free?
                    newMovement.Y = 0;
                }
                else if (end.Y > collisionBox.Top)
                {
                    newMovement.Y = collisionBox.Top - Position.Y;
                    direction = Direction.Up;
                }
                else
                {
                    return movement;
                }
            }
            else if (movement.Y < 0) // Up
            {
                if (Position.Y <= collisionBox.Top)
                {
                    return movement;
                }
                else if (Position.Y < collisionBox.Bottom)
                {
                    //TODO: Move the player out the box? Let im free?
                    newMovement.Y = 0;
                }
                else if (end.Y < collisionBox.Bottom)
                {
                    newMovement.Y = collisionBox.Bottom - Position.Y;
                    direction = Direction.Down;
                }
                else
                {
                    return movement;
                }
            }
            else if (Position.Y <= collisionBox.Top || Position.Y >= collisionBox.Bottom)
            {
                return movement;
            }

            return newMovement;
        }
    }
}
