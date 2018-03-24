namespace dotNES.Renderers
{
    interface IRenderer
    {
       string RendererName { get; }

       void Draw();

       void InitRendering(UI ui);

       void EndRendering();
    }
}
