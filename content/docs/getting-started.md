# Getting Started

The core of the package are **AudioHandles**. 
We can get them by calling:

```cs
AudioHandle handle = Audipool.Get()
```

**AudioHandles** are essentially and interface through which we can manipulate the **AudioSource**.

---

For Example:
```cs
var handle = Audipool.Get()
  .SetResource(audioclip)
  .SetPitch(2)
  .Play()
```
Here we request a handle, set it's resource and play it.

> [!warning]
> **Audipool** automatically releases all handles of **AudioSources** that are not playing. 
> Forgetting to call ``Play()`` can lead to unintentional release of that source.
