import { defineConfig } from "vitepress";

// https://vitepress.dev/reference/site-config
export default defineConfig({
  srcDir: "content",
  base: "/audipool/",
  head: [["link", { rel: "icon", href: "favicon.png" }]],

  title: "Audipool",
  description: "Audipool-unity plugin documentation website",
  themeConfig: {
    search: {
      provider: "local",
    },
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: "Home", link: "/" },
      { text: "Docs", link: "/docs/what-is-audipool" },
    ],

    sidebar: [
      {
        text: "Getting Started",
        base: "/docs/",
        items: [
          { text: "What is Audipool", link: "/what-is-audipool" },
          { text: "Installation", link: "/installation" },
          { text: "Getting Started", link: "/getting-started" },
          { text: "Config", link: "/config" },
        ],
      },
      {
        text: "Reference",
        base: "/docs/reference",
        items: [
          { text: "Audipool", link: "/audipool" },
          {
            text: "Audio Source Data",
            link: "/audio-source-data",
          },
          {
            text: "Audio Preset",
            link: "/audio-preset",
          },
          {
            text: "Audio Handle",
            link: "/audio-handle",
          },
        ],
      },
      {
        text: "Components",
        base: "/docs/components",
        items: [{ text: "Audipool Source", link: "/audipool-source" }],
      },
      {
        text: "Misc",
        items: [
          { text: "Contributing", link: "/docs/contributing" },
          {
            text: "Issues",
            target: "_blank",
            link: "https://github.com/BluePixelDev/audipool/issues",
          },

          {
            text: "License",
            target: "_blank",
            link: "https://github.com/BluePixelDev/audipool/blob/master/LICENSE",
          },
        ],
      },
    ],

    socialLinks: [
      {
        icon: "github",
        link: "https://github.com/BluePixelDev/reave",
      },
    ],

    editLink: {
      pattern: "https://github.com/BluePixelDev/reave/tree/pages/content/:path",
      text: "Edit this page on GitHub",
    },

    footer: { copyright: "Copyright © 2026-present Ondřej Kačírek" },
  },
});
