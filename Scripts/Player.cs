using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Toil.Scripts
{
    public class Player
    {
        public Sprite sprite {get;private set;}
        public List<Input> inputs {get;private set;}
        public Transform transform {get;private set;}
        public string name  {get;private set;}
        public string tag {get;private set;}

        public Player(Sprite _sprite, List<Input> _inputs, string _name, string _tag="Default")
        {
            sprite = _sprite;
            name = _name;
            tag = _tag;
            transform = sprite.transform;
            inputs = new List<Input>(_inputs);
        }

        public void Update(GameTime gameTime){
            foreach (var input in inputs)
            {
                input.Move();
            }

            transform.Update(gameTime);
        }

        public void AddInput(Input _input){
            inputs.Add(_input);
        }

        public void AddInputs(List<Input> _inputs){
            inputs.AddRange(_inputs);
        }

        public void ClearInputs(){
            inputs.Clear();
        }
        public List<Input> GetInputs(){
            return inputs;
        }

        public void RemoveInput(Type type){
            foreach (var input in inputs)
            {
                if(typeof(Input) == type){
                    inputs.Remove(input);
                }
            }
        }
    }
}