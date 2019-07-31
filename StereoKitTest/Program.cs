﻿using System;
using StereoKit;

class Program 
{
    static void Main(string[] args) 
    {
        StereoKitApp kit = new StereoKitApp("StereoKit C#", Runtime.MixedReality);

        Model     gltf   = new Model("Assets/DamagedHelmet.gltf");
        Transform gltfTr = new Transform(Vec3.Zero, Vec3.One*0.5f);
        
        Input.Subscribe(InputSource.Hand, InputState.Any, (src, st, p) => {
            Console.WriteLine("{0} - {1} - {2}", src, st, p.ray.position);
        });

        while (kit.Step(() => {
            Vec3 lookat = new Vec3((float)Math.Cos(StereoKitApp.Time) * 0.5f, 0, (float)Math.Sin(StereoKitApp.Time) * 0.5f);
            
            Hand hand = Input.Hand(Handed.Right);
            if (hand.IsPinched)
                lookat = hand.root.position; 
            
            gltfTr.LookAt(-lookat);
            Renderer.Add(gltf, gltfTr);
        }));
    }
}