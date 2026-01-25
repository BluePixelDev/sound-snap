# `Audipool`

**Namespace:** `BP.Audipool`  
**Type:** `static class`

The central entry point for playing audio through Audipool.
`Audipool` manages audio source pooling and provides a static API for
reserving and playing sounds using [`AudioHandle`][AudioHandle].

This component is initialized early in the Unity lifecycle to ensure
audio is available before dependent systems.

## Description

`Audipool` exposes a static, allocation-friendly interface for creating
and controlling audio playback. Sounds are played by acquiring a
[`AudioHandle`][AudioHandle], configuring it, and starting playback.

Handles are automatically invalidated when playback stops or the
underlying audio slot is recycled.

## Static Methods

### `Get()`

```csharp
public static AudioHandle Get()
```

Reserves an audio slot for manual configuration.

The returned [`AudioHandle`][AudioHandle] can be configured fluently
before starting playback.

* The handle is invalid until `Play()` is called
* Handles are automatically invalidated if playback never starts

**Returns**
A [`AudioHandle`][AudioHandle] used to chain configuration commands.

### `Play(AudioPreset asset)`

```csharp
public static AudioHandle Play(AudioPreset asset)
```

Plays a [`AudioPreset`][AudioPreset] using its stored audio configuration.

This is a convenience method equivalent to:

```csharp
Audipool.Get()
    .WithAsset(asset)
    .Play();
```

**Returns**
A [`AudioHandle`][AudioHandle] used to further configure or control playback.

## Usage Examples

### One-shot Playback

```csharp
Audipool.Play(impactTone);
```

### Manual Configuration

```csharp
Audipool.Get()
    .WithAsset(impactTone)
    .At(hitPoint)
    .Play();
```

### Chaining with Runtime Overrides

```csharp
Audipool.Play(impactTone)
    .Pitch(1.1f)
    .Volume(0.8f);
```

[AudioHandle]: ./audio-handle
[AudioPreset]: ./audio-preset
